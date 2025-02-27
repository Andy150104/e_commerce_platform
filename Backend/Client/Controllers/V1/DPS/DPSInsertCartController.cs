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
/// DPSInsertCartController - Insert Cart
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSInsertCartController : AbstractApiController<DPSInsertCartRequest, DPSInsertCartResponse, string>
{
    private readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSInsertCartController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSInsertCartResponse Post(DPSInsertCartRequest request)
    {
        return Post(request, _context, logger, new DPSInsertCartResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSInsertCartResponse Exec(DPSInsertCartRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSInsertCartResponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get Cart
        var cartSelect = _context.Carts.FirstOrDefault(x => x.Username == userName);
        
        if (cartSelect == null)
        {
            var cart = new Cart
            {
                Username = userName
            };
            _context.Carts.Add(cart);
            _context.SaveChanges(userName);
            cartSelect = cart;
        }
        
        // Get Product
        var productSelect = _context.VwProductDisplays.FirstOrDefault(x => x.ProductId == request.CodeProduct);
        if (request.Quantity > productSelect.Quantity)
        {
            response.SetMessage(MessageId.I00000, "Quantity is greater than stock");
            return response;
        }
        // Get CartItem
        var cartItem = _context.CartItems.FirstOrDefault(item => item.ProductId == productSelect.ProductId && item.CartId == cartSelect.CartId);

        if (cartItem != null)
        {
            cartItem.Quantity = request.Quantity;
            if (cartItem.Quantity == 0)
            {
                // Delete CartItem
                cartItem.IsActive = false;
                
                // Save
                _context.CartItems.Update(cartItem);
                _context.SaveChanges(userName);
                transaction.Commit();
                
                response.Success = true;
                response.SetMessage(MessageId.I00001, "Delete CartItem");
                return response;
            }
            if (!cartItem.IsActive)
            {
                cartItem.IsActive = true;
            }
            _context.CartItems.Update(cartItem);
        }
        else
        {
            // Insert CartItem
            cartItem = new CartItem
            {
                CartId = cartSelect.CartId,
                ProductId = productSelect.ProductId,
                Quantity = request.Quantity
            };
            _context.CartItems.Add(cartItem);
            _context.SaveChanges(userName);
        }
        _context.Carts.Update(cartSelect);
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
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override DPSInsertCartResponse ErrorCheck(DPSInsertCartRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSInsertCartResponse() { Success = false };
        
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