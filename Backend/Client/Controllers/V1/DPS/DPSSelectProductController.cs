using Client.Models.Helper;
using Client.Utils;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSSelectProductController - Select Product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectProductController : AbstractApiControllerNotToken<DPSSelectProductRequest, DPSSelectProductResponse, DPSSelectProductEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSSelectProductController(AppDbContext context)
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
    public override DPSSelectProductResponse Post(DPSSelectProductRequest request)
    {
        return Post(request, _context, logger, new DPSSelectProductResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSSelectProductResponse Exec(DPSSelectProductRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSSelectProductResponse { Success = false };
        
        // Get Product
        var productSelect = _context.VwProductDisplays
            .AsTracking()
            .FirstOrDefault(x => x.ProductId == request.CodeProduct);

        if (productSelect == null)
        {
            response.SetMessage(MessageId.I00000, "Product not found");
            return response;
        }

        // Get ImageList
        var imageList = _context.VwImageProducts
            .AsTracking()
            .Where(x => x.ProductId == productSelect.ProductId)
            .Select(x => new DpsSelectProductListImageUrl
            {
                ImageUrl = x.ImageUrl
            })
            .ToList();

        // Response
        var responseEntity = new DPSSelectProductEntity
        {
            CodeProduct = productSelect.ProductId,
            NameProduct = productSelect.ProductName,
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
            ImageUrls = imageList,
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
    protected internal override DPSSelectProductResponse ErrorCheck(DPSSelectProductRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction) 
    {
        var response = new DPSSelectProductResponse() { Success = false };

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