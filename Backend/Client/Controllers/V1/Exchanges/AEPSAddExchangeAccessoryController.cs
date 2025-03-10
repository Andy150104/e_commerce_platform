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
/// AEPSAddExchangeAccessoryController - Add Exchange Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class AEPSAddExchangeAccessoryController : AbstractApiController<AEPSAddExchangeAccessoryRequest, AEPSAddExchangeAccessoryResponse, string>
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
    public AEPSAddExchangeAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IExchangeService exchangeService)
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
    public override AEPSAddExchangeAccessoryResponse ProcessRequest(AEPSAddExchangeAccessoryRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new AEPSAddExchangeAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner,
         AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override AEPSAddExchangeAccessoryResponse Exec(AEPSAddExchangeAccessoryRequest request)
    {
        return _exchangeService.AddExchangeAccessory(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override AEPSAddExchangeAccessoryResponse ErrorCheck(AEPSAddExchangeAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new AEPSAddExchangeAccessoryResponse() { Success = false };
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