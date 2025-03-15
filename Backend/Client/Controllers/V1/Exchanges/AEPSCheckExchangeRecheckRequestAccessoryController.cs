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
/// AEPSSendExchangeRecheckRequestAccessoryController - Send Re-check request
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class AEPSCheckExchangeRecheckRequestAccessoryController : AbstractApiController<AEPSCheckExchangeRecheckRequestAccessoryRequest, AEPSCheckExchangeRecheckRequestAccessoryResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IExchangeRecheckRequestService _exchangeRecheckRequestService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="exchangeService"></param>
    public AEPSCheckExchangeRecheckRequestAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IExchangeRecheckRequestService exchangeService)
    {
        _identityService = identityService;
        _exchangeRecheckRequestService = exchangeService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override AEPSCheckExchangeRecheckRequestAccessoryResponse ProcessRequest(AEPSCheckExchangeRecheckRequestAccessoryRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new AEPSCheckExchangeRecheckRequestAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.PlannedCustomer + "," + ConstRole.Owner,
         AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override AEPSCheckExchangeRecheckRequestAccessoryResponse Exec(AEPSCheckExchangeRecheckRequestAccessoryRequest request)
    {
        return _exchangeRecheckRequestService.CheckExchange(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override AEPSCheckExchangeRecheckRequestAccessoryResponse ErrorCheck(AEPSCheckExchangeRecheckRequestAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new AEPSCheckExchangeRecheckRequestAccessoryResponse() { Success = false };
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