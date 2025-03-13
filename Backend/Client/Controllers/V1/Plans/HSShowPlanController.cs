using Client.Controllers.AbstractClass;
using Client.Controllers.V1.HS;
using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Plans;

/// <summary>
/// HSShowPlanController - Show Plans Home Screen
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class HSShowPlanController : AbstractApiGetControllerNotToken<HSShowPlanRequest, HSShowPlanResponse, List<HSShowPlanEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IPlanService _planService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="planService"></param>
    public HSShowPlanController(IIdentityService identityService, IPlanService planService)
    {
        _identityService = identityService;
        _planService = planService;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override HSShowPlanResponse Get(HSShowPlanRequest request)
    {
        return Get(request, _identityService, logger, new HSShowPlanResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override HSShowPlanResponse ExecGet(HSShowPlanRequest request)
    {
        return _planService.SelectPlan();
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override HSShowPlanResponse ErrorCheck(HSShowPlanRequest request, List<DetailError> detailErrorList)
    {
        var response = new HSShowPlanResponse() { Success = false };

        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        response.Success = true;
        return response;
    }
}