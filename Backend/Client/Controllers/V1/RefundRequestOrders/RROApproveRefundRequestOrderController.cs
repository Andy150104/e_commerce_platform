using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.RefundRequestOrders;

/// <summary>
/// RROApproveRefundRequestOrderController - Approve refund request order
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class RROApproveRefundRequestOrderController : AbstractApiController<RROApproveRefundRequestOrderRequest, RROApproveRefundRequestOrderResponse, string>
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
    public RROApproveRefundRequestOrderController(IIdentityService identityService, IRefundRequestOrderService refundRequestOrderService, IIdentityApiClient identityApiClient)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _refundRequestOrderService = refundRequestOrderService;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = ConstRole.Owner, AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override RROApproveRefundRequestOrderResponse ProcessRequest(RROApproveRefundRequestOrderRequest request)
    {
        return ProcessRequest(request, _identityService, _logger, new RROApproveRefundRequestOrderResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override RROApproveRefundRequestOrderResponse Exec(RROApproveRefundRequestOrderRequest request)
    {
        return _refundRequestOrderService.ApproveRefundRequestOrder(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override RROApproveRefundRequestOrderResponse ErrorCheck(RROApproveRefundRequestOrderRequest request, List<DetailError> detailErrorList)
    {
        var response = new RROApproveRefundRequestOrderResponse() { Success = false };
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