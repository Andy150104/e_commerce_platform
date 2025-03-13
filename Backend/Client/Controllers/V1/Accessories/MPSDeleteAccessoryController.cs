using Client.Controllers.V1.MPS;
using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.Accessories;

/// <summary>
/// MPSDeleteAccessoryController - Delete Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSDeleteAccessoryController : AbstractApiController<MPSDeleteAccessoryRequest, MPSDeleteAccessoryResponse, string>
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
    public MPSDeleteAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IAccessoryService accessoryService)
    {
        _identityService = identityService;
        _accessoryService = accessoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override MPSDeleteAccessoryResponse ProcessRequest(MPSDeleteAccessoryRequest request)
    {
       return ProcessRequest(request, _identityService, logger, new MPSDeleteAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override MPSDeleteAccessoryResponse Exec(MPSDeleteAccessoryRequest request)
    {
        return _accessoryService.DeleteAccessory(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override MPSDeleteAccessoryResponse ErrorCheck(MPSDeleteAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new MPSDeleteAccessoryResponse() { Success = false };
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