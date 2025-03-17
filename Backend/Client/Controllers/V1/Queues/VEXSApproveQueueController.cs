using Client.Controllers.V1.VEXS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Queues;

/// <summary>
/// VEXSAddToQueueController - Add Exchange Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class VEXSApproveQueueController : AbstractApiController<VEXSApproveQueueRequest, VEXSApproveQueueResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IQueueService _queueService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="queueService"></param>
    public VEXSApproveQueueController(IIdentityApiClient identityApiClient, IIdentityService identityService, IQueueService queueService)
    {
        _identityService = identityService;
        _queueService = queueService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override VEXSApproveQueueResponse ProcessRequest(VEXSApproveQueueRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new VEXSApproveQueueResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override VEXSApproveQueueResponse Exec(VEXSApproveQueueRequest request)
    {
        return _queueService.ApproveQueue(request, _identityService);
    }
    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override VEXSApproveQueueResponse ErrorCheck(VEXSApproveQueueRequest request, List<DetailError> detailErrorList)
    {
        var response = new VEXSApproveQueueResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        
        // True
        response.Success = true;
        return response;
    }
}

