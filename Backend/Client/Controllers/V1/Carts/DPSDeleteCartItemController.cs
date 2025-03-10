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
/// DPSDeleteCartItemController - Delete CartItem
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSDeleteCartItemController : AbstractApiController<DPSDeleteCartItemRequest, DPSDeleteCartItemReponse, string>
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
    public DPSDeleteCartItemController(IIdentityService identityService, IIdentityApiClient identityApiClient, ICartItemService cartItemService)
    {
        _identityService = identityService;
        _cartItemService = cartItemService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSDeleteCartItemReponse ProcessRequest(DPSDeleteCartItemRequest request)
    {
        return ProcessRequest(request, _identityService, _logger, new DPSDeleteCartItemReponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSDeleteCartItemReponse Exec(DPSDeleteCartItemRequest request)
    {
        return _cartItemService.DeleteCartItem(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSDeleteCartItemReponse ErrorCheck(DPSDeleteCartItemRequest request, List<DetailError> detailErrorList)
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