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
/// DPSSelectCartItemController - Select CartItem
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectCartItemController : AbstractApiController<DPSSelectCartItemRequest, DPSSelectCartItemResponse, List<DPSSelectCartItemEntity>>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public DPSSelectCartItemController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override DPSSelectCartItemResponse Post(DPSSelectCartItemRequest request)
    {
        return Post(request, _context, _logger, new DPSSelectCartItemResponse());
    }

    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override DPSSelectCartItemResponse Exec(DPSSelectCartItemRequest request, IDbContextTransaction transaction)
    {
        var response = new DPSSelectCartItemResponse { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get Cart
        var cartSelects = _context.VwCartDisplays
            .AsNoTracking()
            .Where(x => x.CustomerUsername == userName)
            .ToList();
        
        // Get cart result
        var cartItemResponse = new List<DPSSelectCartItemEntity>();
        foreach (var cart in cartSelects)
        {
            var imageSelect = _context.VwImageProducts
                .AsNoTracking()
                .Where(x => x.ProductId == cart.ProductId)
                .Select(x => new DPSSelectCartItemImages
                {
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            var cartEntity = new DPSSelectCartItemEntity
            {
                ProductId = cart.ProductId,
                Price = cart.Price,
                Quantity = cart.Quantity,
                ProductName = cart.ProductName,
                TotalPrice = cart.TotalPrice,
                Images = imageSelect,
                ShortDescription = cart.ShortDescription
            };
            cartItemResponse.Add(cartEntity);
        }
        
        // True
        response.Success = true;
        response.Response = cartItemResponse;
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
    protected internal override DPSSelectCartItemResponse ErrorCheck(DPSSelectCartItemRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new DPSSelectCartItemResponse { Success = false };
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