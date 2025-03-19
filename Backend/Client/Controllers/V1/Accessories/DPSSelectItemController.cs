using Client.Controllers.AbstractClass;
using Client.Controllers.V1.DPS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Accessories;

/// <summary>
/// DPSSelectItemController - Select products
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectItemController : AbstractApiGetControllerNotToken<DPSSelectItemRequest, DPSSelectItemResponse, DPSSelectItemEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IAccessoryService _accessoryService;
    private readonly IExchangeService _exchangeService;
    private readonly IIdentityApiClient _identityApiClient;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="accessoryService"></param>
    /// <param name="exchangeService"></param>
    /// <param name="identityApiClient"></param>
    public DPSSelectItemController(IIdentityService identityService, IAccessoryService accessoryService
        , IExchangeService exchangeService, IIdentityApiClient identityApiClient)
    {
        _identityService = identityService;
        _accessoryService = accessoryService;
        _exchangeService = exchangeService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public override DPSSelectItemResponse Get(DPSSelectItemRequest request)
    {
        return Get(request, _identityService, logger, new DPSSelectItemResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSSelectItemResponse ExecGet(DPSSelectItemRequest request)
    {
        var response = new DPSSelectItemResponse() { Success = false };
        var responseEntity = new List<ItemEntity>();
        
        #region SelectItem
        
        // Get request

        #region Variable declaration
        decimal? minPrice = request.MinimumPrice.HasValue ? request.MinimumPrice.Value : null;
        decimal? maxPrice = request.MaximumPrice.HasValue ? request.MaximumPrice.Value : null;
        string parentCategory = request.ParentCategoryName?.Trim().ToLower()!;
        string childCategory = request.ChildCategoryName?.Trim().ToLower()!;
        string nameAccessory = request.NameAccessory?.Trim().ToLower()!;
        
        #endregion
        
        request.CurrentPage = Math.Max(1, request.CurrentPage);
        request.PageSize = Math.Max(1, request.PageSize);
        
        // Get userName
        var userName = _identityApiClient?.GetIdentity(User)?.UserName;
        
        // If search by product
        if (request.SearchBy == (byte) ConstantEnum.ProductType.Product)
        {
            // Select products
           responseEntity = _accessoryService.SelectByAccessory( minPrice, maxPrice, nameAccessory!, parentCategory!, childCategory!, request.SortBy, userName!);
        }
        
        // If search by blind box
        else if (request.SearchBy == (byte) ConstantEnum.ProductType.BlindBox)
        {
            responseEntity = _exchangeService.SelectByBlindBox(request.SortBy);
        }

        // Total records
        var totalRecords = responseEntity.Count;
        
        // Paging
        var itemDisplay = responseEntity
            .Skip((request.CurrentPage - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
        
        // Set response
        var entityReturn = new DPSSelectItemEntity
        {
            Items = itemDisplay,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
        };
        
        #endregion
        
        // True
        response.Success = true;
        response.Response = entityReturn;
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSSelectItemResponse ErrorCheck(DPSSelectItemRequest request, List<DetailError> detailErrorList)
    {
        var response = new DPSSelectItemResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        
        // True
        response.Success = true;
        return response;
    }
}