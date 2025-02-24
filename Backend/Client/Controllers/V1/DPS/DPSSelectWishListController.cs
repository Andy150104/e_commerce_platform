using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSSelectWishListController - Select WishList
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectWishListController : AbstractApiController<DPSSelectWishListRequest, DPSSelectWishListResponse, List<DPSSelectWishListEntity>>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSSelectWishListController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = _logger;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSSelectWishListResponse Post(DPSSelectWishListRequest itemRequest)
    {
        return Post(itemRequest, _context, _logger, new DPSSelectWishListResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSSelectWishListResponse Exec(DPSSelectWishListRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSSelectWishListResponse { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get WishList
        var wishListSelects = _context.VwWishListDisplays
            .AsNoTracking()
            .Where(x => x.CustomerUsername == userName)
            .ToList();
        
        // Set Response
        var wishListResponse = new List<DPSSelectWishListEntity>();
        foreach (var wishList in wishListSelects)
        {
            var imageSelects = _context.VwImageProducts
                .AsNoTracking()
                .Where(x => x.ProductId == wishList.ProductId)
                .Select(x => new DPSSelectWishListImages
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();
            
            var wishListEntity = new DPSSelectWishListEntity
            {
                ProductId = wishList.ProductId,
                ProductName = wishList.ProductName,
                ShortDescription = wishList.ShortDescription,
                Images = imageSelects,
            };
            wishListResponse.Add(wishListEntity);
        }
        
        // True
        response.Success = true;
        response.Response = wishListResponse;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override DPSSelectWishListResponse ErrorCheck(DPSSelectWishListRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSSelectWishListResponse() { Success = false };
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