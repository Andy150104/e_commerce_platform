using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Orders;

/// <summary>
/// TrackingGhnOrderController - Tracking Ghn Order
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class TrackingGhnOrderController : AbstractApiAsyncController<TrackingGhnOrderRequest, TrackingGhnOrderResponse, GhnOrderResponse>
{
    private readonly IIdentityService _identityService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IOrderService _orderService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="orderService"></param>
    public TrackingGhnOrderController(IIdentityService identityService, IIdentityApiClient identityApiClient, IOrderService orderService)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _orderService = orderService;
    }

    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<TrackingGhnOrderResponse> Post(TrackingGhnOrderRequest filter)
    {
        return await Post(filter, _identityService, _logger, new TrackingGhnOrderResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    protected override async Task<TrackingGhnOrderResponse> Exec(TrackingGhnOrderRequest filter)
    {
        return await _orderService.TrackingOrderAsync(filter, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override TrackingGhnOrderResponse ErrorCheck(TrackingGhnOrderRequest request, List<DetailError> detailErrorList)
    {
        var response = new TrackingGhnOrderResponse() { Success = false };
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