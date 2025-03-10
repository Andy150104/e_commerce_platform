using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSInsertCartController - Insert Cart
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSInsertCartController : AbstractApiController<DPSInsertCartRequest, DPSInsertCartResponse, string>
{
    private readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly ICartItemService _cartItemService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="cartItemService"></param>
    public DPSInsertCartController(IIdentityService identityService, IIdentityApiClient identityApiClient, ICartItemService cartItemService)
    {
        _identityService = identityService;
        _cartItemService = cartItemService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSInsertCartResponse ProcessRequest(DPSInsertCartRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new DPSInsertCartResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSInsertCartResponse Exec(DPSInsertCartRequest request)
    {
        return _cartItemService.InsertCartItem(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSInsertCartResponse ErrorCheck(DPSInsertCartRequest request, List<DetailError> detailErrorList)
    {
        var response = new DPSInsertCartResponse() { Success = false };
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