using Client.Controllers.V1.MPS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Accessories;

/// <summary>
/// MPSDeleteImageAccessoryController - Delete image accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSDeleteImageAccessoryController : AbstractApiController<MPSDeleteImageAccessoryRequest, MPSDeleteImageAccessoryResponse, string>
{    
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IAccessoryService _accessoryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="accessoryService"></param>
    public MPSDeleteImageAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IAccessoryService accessoryService)
    {
        _identityService = identityService;
        _accessoryService = accessoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override MPSDeleteImageAccessoryResponse ProcessRequest(MPSDeleteImageAccessoryRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new MPSDeleteImageAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override MPSDeleteImageAccessoryResponse Exec(MPSDeleteImageAccessoryRequest request)
    {
        return _accessoryService.DeleteImageAccessory(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override MPSDeleteImageAccessoryResponse ErrorCheck(MPSDeleteImageAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new MPSDeleteImageAccessoryResponse() { Success = false };
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        // True
        response.Success = true;
        return response;
    }
}