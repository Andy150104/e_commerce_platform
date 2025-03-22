using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.ThirdParty;

/// <summary>
/// MomoOrderLogicReturnController - Use for Momo after payment return
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class MomoOrderLogicReturnController : AbstractApiController<MomoOrderLogicReturnRequest, MomoOrderLogicReturnResponse, string>
{
    private readonly IIdentityService _identityService;
    private readonly IOrderService _orderService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="orderService"></param>
    /// <param name="identityApiClient"></param>
    public MomoOrderLogicReturnController(IIdentityService identityService, IOrderService orderService, IIdentityApiClient identityApiClient)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _orderService = orderService;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override MomoOrderLogicReturnResponse ProcessRequest(MomoOrderLogicReturnRequest request)
    {
        return ProcessRequest(request, _identityService, _logger, new MomoOrderLogicReturnResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override MomoOrderLogicReturnResponse Exec(MomoOrderLogicReturnRequest request)
    {
        return _orderService.UpdateOrderStatusBySystem(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override MomoOrderLogicReturnResponse ErrorCheck(MomoOrderLogicReturnRequest request, List<DetailError> detailErrorList)
    {
        var response = new MomoOrderLogicReturnResponse() { Success = false };

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