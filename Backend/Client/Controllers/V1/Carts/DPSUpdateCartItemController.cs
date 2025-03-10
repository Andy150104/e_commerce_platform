using Client.Controllers.V1.DPS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Carts;

/// <summary>
/// DPSUpdateCartItemController - Update CartItem
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSUpdateCartItemController : AbstractApiController<DPSUpdateCartItemRequest, DPSUpdateCartItemResponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly ICartItemService _cartItemService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="cartItemService"></param>
    public DPSUpdateCartItemController(IIdentityService identityService, IIdentityApiClient identityApiClient, ICartItemService cartItemService)
    {
        _identityService = identityService;
        _cartItemService = cartItemService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSUpdateCartItemResponse ProcessRequest(DPSUpdateCartItemRequest itemRequest)
    {
        return ProcessRequest(itemRequest, _identityService, _logger, new DPSUpdateCartItemResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <returns></returns>
    protected override DPSUpdateCartItemResponse Exec(DPSUpdateCartItemRequest itemRequest)
    {
        return _cartItemService.UpdateCartItem(itemRequest, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <param name="detailErrorList"></param>
    /// /// <returns></returns>
    protected internal override DPSUpdateCartItemResponse ErrorCheck(DPSUpdateCartItemRequest itemRequest, List<DetailError> detailErrorList)
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