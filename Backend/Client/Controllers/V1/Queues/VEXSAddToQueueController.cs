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
public class VEXSAddToQueueController : AbstractApiController<VEXSAddToQueueRequest, VEXSAddToQueueResponse, string>
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
    public VEXSAddToQueueController(IIdentityApiClient identityApiClient, IIdentityService identityService, IQueueService queueService)
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
    public override VEXSAddToQueueResponse ProcessRequest(VEXSAddToQueueRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new VEXSAddToQueueResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override VEXSAddToQueueResponse Exec(VEXSAddToQueueRequest request)
    {
        return _queueService.AddToQueue(request, _identityService);
    }
    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override VEXSAddToQueueResponse ErrorCheck(VEXSAddToQueueRequest request, List<DetailError> detailErrorList)
    {
        var response = new VEXSAddToQueueResponse() { Success = false };
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

