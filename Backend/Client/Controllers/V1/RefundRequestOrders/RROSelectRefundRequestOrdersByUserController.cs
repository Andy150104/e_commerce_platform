using Client.Controllers.AbstractClass;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.RefundRequestOrders;

/// <summary>
/// RROSelectRefundRequestOrdersByUserController - Select refund request orders by user
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class RROSelectRefundRequestOrdersByUserController: AbstractApiGetController<RROSelectRefundRequestOrdersRequest, RROSelectRefundRequestOrdersResponse, List<RROSelectRefundRequestOrdersEntity>>
{
    
    private readonly IIdentityService _identityService;
    private readonly IRefundRequestOrderService _refundRequestOrderService;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="refundRequestOrderService"></param>
    /// <param name="identityApiClient"></param>
    public RROSelectRefundRequestOrdersByUserController(IIdentityService identityService, IRefundRequestOrderService refundRequestOrderService, IIdentityApiClient identityApiClient)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _refundRequestOrderService = refundRequestOrderService;
    }
    
    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = ConstRole.Customer + "," + ConstRole.PlannedCustomer, AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override RROSelectRefundRequestOrdersResponse Get(RROSelectRefundRequestOrdersRequest filter)
    {
        return Get(filter, _identityService, _logger, new RROSelectRefundRequestOrdersResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    protected override RROSelectRefundRequestOrdersResponse ExecGet(RROSelectRefundRequestOrdersRequest filter)
    {
        return _refundRequestOrderService.SelectRefundRequestOrders(_identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override RROSelectRefundRequestOrdersResponse ErrorCheck(RROSelectRefundRequestOrdersRequest request, List<DetailError> detailErrorList)
    {
        var response = new RROSelectRefundRequestOrdersResponse() { Success = false };
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