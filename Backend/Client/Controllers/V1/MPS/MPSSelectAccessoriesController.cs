using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.MPS;

/// <summary>
/// MPSSelectAccessoriesController - Select Accessories
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSSelectAccessoriesController : AbstractApiController<MPSSelectAccessoriesRequest, MPSSelectAccessoriesResponse, List<MPSSelectAccessoriesEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public MPSSelectAccessoriesController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override MPSSelectAccessoriesResponse Post(MPSSelectAccessoriesRequest request)
    {
        return Post(request, _context, logger, new MPSSelectAccessoriesResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override MPSSelectAccessoriesResponse Exec(MPSSelectAccessoriesRequest request, IDbContextTransaction transaction)
    {
        var response = new MPSSelectAccessoriesResponse { Success = false };
        var mpsSelectAccessoriesEntity = new List<MPSSelectAccessoriesEntity>();

        // Get Accessory
        var accessorySelects = _context.VwAccessoryDisplays.AsNoTracking().ToList();

        foreach (var accessory in accessorySelects)
        {
            // Get Image Accessory
            var imageAccessory = _context.VwImageAccessories
                .AsNoTracking()
                .Where(img => img.AccessoryId == accessory.AccessoryId)
                .Select( x => new MPSSelectImageAccessories
                {
                    AccessoryId = x.AccessoryId,
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                })
                .ToList();
            
            // Response
            var entity = new MPSSelectAccessoriesEntity
            {
                AccessoryId = accessory.AccessoryId,
                AccessoryName = accessory.AccessoryName,
                Price = accessory.Price,
                Description = accessory.Description,
                ShortDescription = accessory.ShortDescription,
                CreatedAt = accessory.UpdatedAt,
                Discount = accessory.Discount,
                Quantity = accessory.Quantity,
                AverageRating = accessory.AverageRating,
                TotalOrders = accessory.TotalOrders,
                TotalReviews = accessory.TotalReviews,
                TotalSold = accessory.TotalSold,
                ChildCategoryName = accessory.ChildCategoryName,
                ParentCategoryName = accessory.ParentCategoryName,
                ImageAccessoriesList = imageAccessory,
            };
            mpsSelectAccessoriesEntity.Add(entity);
        }
        
        // True
        response.Success = true;
        response.Response = mpsSelectAccessoriesEntity;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override MPSSelectAccessoriesResponse ErrorCheck(MPSSelectAccessoriesRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new MPSSelectAccessoriesResponse() { Success = false };
        
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