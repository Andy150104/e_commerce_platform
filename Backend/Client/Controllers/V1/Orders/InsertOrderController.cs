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
/// TOSInsertOrderController - Insert new Order
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class InsertOrderController : AbstractApiAsyncController<InsertOrderRequest, InsertOrderResponse, InsertOrderResponseEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    public InsertOrderController(IIdentityService identityService, IIdentityApiClient identityApiClient, IOrderService orderService)
    {
        _identityService = identityService;
        _orderService = orderService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public async override Task<InsertOrderResponse> Post(InsertOrderRequest request)
    {
        return await Post(request, _identityService, logger, new InsertOrderResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected async override Task<InsertOrderResponse> Exec(InsertOrderRequest request)
    {
        return await _orderService.InsertOrder(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override InsertOrderResponse ErrorCheck(InsertOrderRequest request, List<DetailError> detailErrorList)
    {
        var response = new InsertOrderResponse() { Success = false };

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