using Client.Controllers.AbstractClass;
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
/// DPSSelectCartItemController - Select CartItem
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectCartItemController : AbstractApiGetController<DPSSelectCartItemRequest, DPSSelectCartItemResponse, List<DPSSelectCartItemEntity>>
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
    public DPSSelectCartItemController(IIdentityService identityService, IIdentityApiClient identityApiClient, ICartItemService cartItemService)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _cartItemService = cartItemService;
    }

    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSSelectCartItemResponse Get(DPSSelectCartItemRequest request)
    {
        return Get(request, _identityService, _logger, new DPSSelectCartItemResponse());
    }

    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSSelectCartItemResponse ExecGet(DPSSelectCartItemRequest request)
    {
        return _cartItemService.SelectCartItem(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSSelectCartItemResponse ErrorCheck(DPSSelectCartItemRequest request, List<DetailError> detailErrorList)
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