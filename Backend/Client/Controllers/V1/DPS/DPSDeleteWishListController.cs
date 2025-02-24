using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSDeleteWishListController - Delete wishlist item
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSDeleteWishListController : AbstractApiController<DPSDeleteWishListRequest, DPSDeleteWishListResponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSDeleteWishListController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = _logger;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSDeleteWishListResponse Post(DPSDeleteWishListRequest request)
    {
        return Post(request, _context, _logger, new DPSDeleteWishListResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSDeleteWishListResponse Exec(DPSDeleteWishListRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSDeleteWishListResponse { Success = false };
        
        // Get UserName
        var userName = _context.IdentityEntity.UserName;
        
        // Get WishList
        var wishListSelect = _context.Wishlists.FirstOrDefault(x => x.UserName == userName);
        
        // Get WishListItem
        var wishListItemSelect = _context.WishlistItems.FirstOrDefault(x => x.WishlistId == wishListSelect.WishlistId 
                                                                                    && x.ProductId == request.CodeProduct
                                                                                    && x.IsActive == true);

        wishListItemSelect.IsActive = false;
        _context.WishlistItems.Update(wishListItemSelect);
        _context.SaveChanges(userName);
        
        // True
        response.Success = true;
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
    protected internal override DPSDeleteWishListResponse ErrorCheck(DPSDeleteWishListRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSDeleteWishListResponse() { Success = false };

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