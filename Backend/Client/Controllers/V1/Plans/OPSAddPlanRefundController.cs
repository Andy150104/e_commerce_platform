using Client.Controllers.V1.OnlinePaymentScreen;
using Client.Controllers.V1.OPS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Plans;

/// <summary>
/// OPSAddPlanRefundController - buying plan
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class OPSAddPlanRefundController : AbstractApiController<OPSAddPlanRefundRequest, OPSAddPlanRefundResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IPlanService _planService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="planService"></param>
    public OPSAddPlanRefundController(IIdentityService identityService, IIdentityApiClient identityApiClient, IPlanService planService)
    {
        _identityService = identityService;
        _planService = planService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override OPSAddPlanRefundResponse ProcessRequest(OPSAddPlanRefundRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new OPSAddPlanRefundResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override OPSAddPlanRefundResponse Exec(OPSAddPlanRefundRequest request)
    {
        return _planService.AddPlanRefund(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailerrorlist"></param>
    /// <returns></returns>
    protected internal override OPSAddPlanRefundResponse ErrorCheck(OPSAddPlanRefundRequest request, List<DetailError> detailerrorlist)
    {
        var response = new OPSAddPlanRefundResponse() { Success = false };
        if (detailerrorlist.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailerrorlist;
            return response;
        }

        // True
        response.Success = true;
        return response;
    }
}