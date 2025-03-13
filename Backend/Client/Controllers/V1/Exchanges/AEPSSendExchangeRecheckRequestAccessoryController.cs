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
public class AEPSSendExchangeRecheckRequestAccessoryController : AbstractApiController<AEPSSendExchangeRecheckRequestAccessoryRequest, AEPSSendExchangeRecheckRequestAccessoryResponse, string>
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
    public AEPSSendExchangeRecheckRequestAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IExchangeRecheckRequestService exchangeService)
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
    public override AEPSSendExchangeRecheckRequestAccessoryResponse ProcessRequest(AEPSSendExchangeRecheckRequestAccessoryRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new AEPSSendExchangeRecheckRequestAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.PlannedCustomer + "," + ConstRole.Owner,
         AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override AEPSSendExchangeRecheckRequestAccessoryResponse Exec(AEPSSendExchangeRecheckRequestAccessoryRequest request)
    {
        return _exchangeRecheckRequestService.SendReCheck(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override AEPSSendExchangeRecheckRequestAccessoryResponse ErrorCheck(AEPSSendExchangeRecheckRequestAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new AEPSSendExchangeRecheckRequestAccessoryResponse() { Success = false };
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