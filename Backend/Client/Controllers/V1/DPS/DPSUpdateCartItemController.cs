using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSUpdateCartItemController - Update CartItem
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSUpdateCartItemController : AbstractApiController<DPSUpdateCartItemRequest, DPSUpdateCartItemResponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSUpdateCartItemController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override DPSUpdateCartItemResponse Post(DPSUpdateCartItemRequest itemRequest)
    {
        return Post(itemRequest, _context, _logger, new DPSUpdateCartItemResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSUpdateCartItemResponse Exec(DPSUpdateCartItemRequest itemRequest, IDbContextTransaction transaction)
    {
        var response = new DPSUpdateCartItemResponse { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get Cart
        var cartSelect = _context.Carts.FirstOrDefault(x => x.Username == userName);
        
        // Get CartItem
        var cartItemSelect = _context.CartItems.FirstOrDefault(x => x.CartId == cartSelect.CartId && x.AccessoryId == itemRequest.AccessoryId);
        
        // Update CartItem
        cartItemSelect.Quantity = itemRequest.Quantity;
        if (itemRequest.Quantity <= 0)
        {
            cartItemSelect.Quantity = 0;
            cartItemSelect.IsActive = false;
        }
        _context.CartItems.Update(cartItemSelect);
        _context.SaveChanges(userName);
        transaction.Commit();
        
        // True
        response.Success = true;
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
    protected internal override DPSUpdateCartItemResponse ErrorCheck(DPSUpdateCartItemRequest itemRequest, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSUpdateCartItemResponse() { Success = false };

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