using Client.Controllers.AbstractClass;
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
/// TOSSelectOrdersController - Select Orders
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class SelectOrdersController : AbstractApiGetController<SelectOrdersRequest, SelectOrdersResponse, List<SelectOrdersEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    public SelectOrdersController(IIdentityService identityService, IIdentityApiClient identityApiClient, IOrderService orderService)
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
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override SelectOrdersResponse Get(SelectOrdersRequest request)
    {
        return Get(request, _identityService, logger, new SelectOrdersResponse());
    }

    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override SelectOrdersResponse ExecGet(SelectOrdersRequest request)
    {
        return _orderService.SelectOrders(_identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override SelectOrdersResponse ErrorCheck(SelectOrdersRequest request, List<DetailError> detailErrorList)
    {
        var response = new SelectOrdersResponse() { Success = false };
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