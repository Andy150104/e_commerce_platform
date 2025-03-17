using Azure.Core;
using Client.Controllers.AbstractClass;
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
/// AEPSGetExchangeAccessoryController - Get Exchange Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class VEXSGetOrderExchangeController : AbstractApiGetController<VEXSGetOrderExchangeRequest, VEXSGetOrderExchangeResponse, List<VEXSGetOrderExchangeResponseEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IQueueService _queueService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="exchangeService"></param>
    public VEXSGetOrderExchangeController(IIdentityService identityService, IIdentityApiClient identityApiClient, IQueueService queueService)
    {
        _identityService = identityService;
        _queueService = queueService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override VEXSGetOrderExchangeResponse Get(VEXSGetOrderExchangeRequest request)
    {
        return Get(request, _identityService, logger, new VEXSGetOrderExchangeResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = ConstRole.PlannedCustomer + "," + ConstRole.Owner,
     AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override VEXSGetOrderExchangeResponse ExecGet(VEXSGetOrderExchangeRequest request)
    {
        return _queueService.GetOrderExchange(request, _identityService);
    }

    /// <summary>
    /// Error check 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override VEXSGetOrderExchangeResponse ErrorCheck(VEXSGetOrderExchangeRequest request, List<DetailError> detailErrorList)
    {
        var response = new VEXSGetOrderExchangeResponse() { Success = false };
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