using Client.Models;
using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSInsertWishListController - Add wishlist product
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSInsertWishListController : AbstractApiController<DPSInsertWishListRequest, DPSInsertWishListResponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public DPSInsertWishListController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSInsertWishListResponse Post(DPSInsertWishListRequest request)
    {
        return Post(request, _context, _logger, new DPSInsertWishListResponse());
    }

    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSInsertWishListResponse Exec(DPSInsertWishListRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSInsertWishListResponse { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get WishList
        var wishListSelect = _context.Wishlists.FirstOrDefault(x => x.UserName == userName);
        if (wishListSelect == null)
        {
            // Create new wishList
            var wishList = new Wishlist
            {
                UserName = userName,
            };
            _context.Wishlists.Add(wishList);
            _context.SaveChanges(userName);
        }

        // WishListItem Select
        var wishListItem = _context.WishlistItems.FirstOrDefault(item => item.ProductId == request.CodeProduct);

        if (wishListItem == null)
        {
            var newWishListItem = new WishlistItem
            {
                ProductId = request.CodeProduct,
                WishlistId = wishListSelect.WishlistId,
            };
            _context.WishlistItems.Add(newWishListItem);
        }
        else
        {
            if (!wishListItem.IsActive)
            {
                wishListItem.IsActive = true;
            }
            _context.WishlistItems.Update(wishListItem);
        }
        _context.SaveChanges(userName);
        transaction.Commit();
        
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
    protected internal override DPSInsertWishListResponse ErrorCheck(DPSInsertWishListRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSInsertWishListResponse() { Success = false };
        
        // Get Product
        var productSelect = _context.VwProductDisplays.FirstOrDefault(x => x.ProductId == request.CodeProduct);
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