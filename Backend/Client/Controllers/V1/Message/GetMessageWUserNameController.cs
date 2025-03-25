using Azure.Core;
using Client.Controllers.AbstractClass;
using Client.Controllers.V1.AEPS;
using Client.Controllers.V1.Message;
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
public class GetMessageWUserNameController : AbstractApiGetController<GetMessageWUserNameRequest, GetMessageWUserNameResponse, List<GetMessageWEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IMessageService _exchangeService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="exchangeService"></param>
    public GetMessageWUserNameController(IIdentityService identityService, IIdentityApiClient identityApiClient, IMessageService exchangeService)
    {
        _identityService = identityService;
        _exchangeService = exchangeService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override GetMessageWUserNameResponse Get(GetMessageWUserNameRequest request)
    {
        return Get(request, _identityService, logger, new GetMessageWUserNameResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = ConstRole.PlannedCustomer + "," + ConstRole.Owner,
     AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override GetMessageWUserNameResponse ExecGet(GetMessageWUserNameRequest request)
    {
        return _exchangeService.GetMessageWEntity(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override GetMessageWUserNameResponse ErrorCheck(GetMessageWUserNameRequest request, List<DetailError> detailErrorList)
    {
        var response = new GetMessageWUserNameResponse() { Success = false };
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