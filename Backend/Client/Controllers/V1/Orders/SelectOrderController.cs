using Client.Controllers.AbstractClass;
using Client.Controllers.V1.TOS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Orders;

/// <summary>
/// TOSSelectOrdersController - Select Orders
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class SelectOrderController : AbstractApiGetController<SelectOrderRequest, SelectOrderResponse, SelectOrderEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    public SelectOrderController(IIdentityService identityService, IIdentityApiClient identityApiClient, IOrderService orderService)
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
    public override SelectOrderResponse Get(SelectOrderRequest request)
    {
        return Get(request, _identityService, logger, new SelectOrderResponse());
    }

    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override SelectOrderResponse ExecGet(SelectOrderRequest request)
    {
        return _orderService.SelectOrder(request, _identityService);
    }
    
    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override SelectOrderResponse ErrorCheck(SelectOrderRequest request, List<DetailError> detailErrorList)
    {
        var response = new SelectOrderResponse() { Success = false };
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