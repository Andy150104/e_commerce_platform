using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.OPS;

/// <summary>
/// OPSBuyingPlanController - Buying Plan
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class OPSBuyingPlanController : AbstractApiController<OPSBuyingPlanRequest, OPSBuyingPlanResponse, string>
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
    public OPSBuyingPlanController(IIdentityService identityService, IIdentityApiClient identityApiClient, IPlanService planService)
    {
        _identityService = identityService;
        _planService = planService;
        _identityApiClient = identityApiClient;
    }
    
    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override OPSBuyingPlanResponse ProcessRequest(OPSBuyingPlanRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new OPSBuyingPlanResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override OPSBuyingPlanResponse Exec(OPSBuyingPlanRequest request)
    {
        return _planService.BuyingPlan(request, _identityService);
    }
    
    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailerrorlist"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override OPSBuyingPlanResponse ErrorCheck(OPSBuyingPlanRequest request, List<DetailError> detailerrorlist)
    {
        var response = new OPSBuyingPlanResponse() { Success = false };
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

