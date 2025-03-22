using Client.Controllers.V1.AEPS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Exchanges;

/// <summary>
/// AEPSFinalAcceptExchangeAccessoryController - Send Re-check request
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class AEPSFinalAcceptExchangeAccessoryController : AbstractApiController<AEPSFinalAcceptExchangeAccessoryRequest, AEPSFinalAcceptExchangeAccessoryResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IExchangeService _exchangeService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="exchangeService"></param>
    public AEPSFinalAcceptExchangeAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IExchangeService exchangeService)
    {
        _identityService = identityService;
        _exchangeService = exchangeService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override AEPSFinalAcceptExchangeAccessoryResponse ProcessRequest(AEPSFinalAcceptExchangeAccessoryRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new AEPSFinalAcceptExchangeAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.PlannedCustomer + "," + ConstRole.Owner,
         AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override AEPSFinalAcceptExchangeAccessoryResponse Exec(AEPSFinalAcceptExchangeAccessoryRequest request)
    {
        return _exchangeService.FinalAcceptedExchange(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override AEPSFinalAcceptExchangeAccessoryResponse ErrorCheck(AEPSFinalAcceptExchangeAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new AEPSFinalAcceptExchangeAccessoryResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        //true
        response.Success = true;
        return response;
    }
}