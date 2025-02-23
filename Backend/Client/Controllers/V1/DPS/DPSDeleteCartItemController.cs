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
/// DPSDeleteCartItemController - Delete CartItem
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSDeleteCartItemController : AbstractApiController<DPSDeleteCartItemRequest, DPSDeleteCartItemReponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSDeleteCartItemController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override DPSDeleteCartItemReponse Post(DPSDeleteCartItemRequest request)
    {
        return Post(request, _context, _logger, new DPSDeleteCartItemReponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSDeleteCartItemReponse Exec(DPSDeleteCartItemRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSDeleteCartItemReponse { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get Cart
        var cartSelect = _context.Carts.FirstOrDefault(x => x.Username == userName);
        
        // Get CartItem
        var cartItemSelect = _context.CartItems.FirstOrDefault(x => x.CartId == cartSelect.CartId 
                                                                    && x.ProductId == request.CodeProduct
                                                                    && x.IsActive == true);
        
        // Delete CartItem
        cartItemSelect.IsActive = false;
        cartItemSelect.Quantity = 0;
        _context.CartItems.Update(cartItemSelect);
        _context.SaveChanges(userName);
        
        // True
        response.Success = true;
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
    protected internal override DPSDeleteCartItemReponse ErrorCheck(DPSDeleteCartItemRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSDeleteCartItemReponse() { Success = false };

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