using Client.Controllers.V1.TOS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Orders;

/// <summary>
/// PaymentOrderCallbackController - Payment Order Callback
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class PaymentOrderCallbackController : AbstractApiAsyncController<PaymentOrderCallbackRequest, PaymentOrderCallbackResponse, MomoResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IOrderService _orderService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="orderService"></param>
    public PaymentOrderCallbackController(IIdentityService identityService, IIdentityApiClient identityApiClient, IOrderService orderService)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _orderService = orderService;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<PaymentOrderCallbackResponse> Post(PaymentOrderCallbackRequest request)
    {
        return await Post(request, _identityService, _logger, new PaymentOrderCallbackResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<PaymentOrderCallbackResponse> Exec(PaymentOrderCallbackRequest request)
    {
        return await _orderService.PaymentOrderCallback(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override PaymentOrderCallbackResponse ErrorCheck(PaymentOrderCallbackRequest request, List<DetailError> detailErrorList)
    {
        var response = new PaymentOrderCallbackResponse() { Success = false };

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