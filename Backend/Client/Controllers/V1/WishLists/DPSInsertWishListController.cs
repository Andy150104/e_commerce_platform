using Client.Controllers.V1.DPS;
using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.WishLists;

/// <summary>
/// DPSInsertWishListController - Add wishlist product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSInsertWishListController : AbstractApiController<DPSInsertWishListRequest, DPSInsertWishListResponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IWishListService _wishListService;
    private readonly IAccessoryService _accessoryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="wishListService"></param>
    /// <param name="accessoryService"></param>
    public DPSInsertWishListController(AppDbContext context, IIdentityApiClient identityApiClient, 
        IIdentityService identityService, IWishListService wishListService, IAccessoryService accessoryService)
    {
        _identityService = identityService;
        _wishListService = wishListService;
        _accessoryService = accessoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSInsertWishListResponse ProcessRequest(DPSInsertWishListRequest request)
    {
        return ProcessRequest(request, _identityService, _logger, new DPSInsertWishListResponse());
    }

    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSInsertWishListResponse Exec(DPSInsertWishListRequest request)
    {
        return _wishListService.InsertWishList(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override DPSInsertWishListResponse ErrorCheck(DPSInsertWishListRequest request, List<DetailError> detailErrorList)
    {
        var response = new DPSInsertWishListResponse() { Success = false };
        
        // Get Accessory
        var accessory = _accessoryService.Find(x => x.AccessoryId == request.CodeAccessory && x.IsActive == true);
        if (accessory == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
            response.DetailErrorList = detailErrorList;
            return response;
        }

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