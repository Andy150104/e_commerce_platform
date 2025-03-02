using Client.Models.Helper;
using Client.Utils;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSSelectAccessoryController - Select Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectAccessoryController : AbstractApiControllerNotToken<DPSSelectAccessoryRequest, DPSSelectAccessoryResponse, DPSSelectAccessoryEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSSelectAccessoryController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }
    
    /// <summary>
    /// Incoming Post 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public override DPSSelectAccessoryResponse Post(DPSSelectAccessoryRequest request)
    {
        return Post(request, _context, logger, new DPSSelectAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSSelectAccessoryResponse Exec(DPSSelectAccessoryRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSSelectAccessoryResponse { Success = false };
        
        // Get Accessory
        var productSelect = _context.VwAccessoryDisplays
            .AsTracking()
            .FirstOrDefault(x => x.AccessoryId == request.CodeAccessory);

        // Get ImageList
        var imageList = _context.VwImageAccessories
            .AsTracking()
            .Where(x => x.AccessoryId == productSelect.AccessoryId)
            .Select(x => new DpsSelectAccessoryListImageUrl
            {
                ImageUrl = x.ImageUrl
            })
            .ToList();

        // Response
        var responseEntity = new DPSSelectAccessoryEntity
        {
            CodeAccessory = productSelect!.AccessoryId,
            NameAccessory = productSelect.AccessoryName,
            ShortDescription = productSelect.ShortDescription,
            Description = productSelect.Description,
            Price = StringUtil.ConvertToVND(productSelect.Price),
            Discount = StringUtil.ConvertToPercent(productSelect.Discount),
            SalePrice = (productSelect.Price * (1 - productSelect.Discount / 100)).ToString(),
            CreatedAt = productSelect.CreatedAt,
            AverageRating = productSelect.AverageRating,
            TotalReviews = productSelect.TotalReviews,
            ChildCategoryName = productSelect.ChildCategoryName,
            ParentCategoryName = productSelect.ParentCategoryName,
            //ImageUrls = imageList,
        };
        
        // True
        response.Success = true;
        response.Response = responseEntity;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override DPSSelectAccessoryResponse ErrorCheck(DPSSelectAccessoryRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction) 
    {
        var response = new DPSSelectAccessoryResponse() { Success = false };
        var productSelect = _context.VwAccessoryDisplays
            .AsTracking()
            .FirstOrDefault(x => x.AccessoryId == request.CodeAccessory);
        
        if (productSelect == null)
        {
            response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
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