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
/// MPSUpdateAccessoryController - Update Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSUpdateAccessoryController : AbstractApiAsyncController<MPSUpdateAccessoryRequest, MPSUpdateAccessoryResponse, string>
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
    public MPSUpdateAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IAccessoryService accessoryService)
    {
        _identityService = identityService;
        _accessoryService = accessoryService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Put
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override Task<MPSUpdateAccessoryResponse> Post(MPSUpdateAccessoryRequest request)
    {
        return Post(request, _identityService, logger, new MPSUpdateAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<MPSUpdateAccessoryResponse> Exec(MPSUpdateAccessoryRequest request)
    {
       return await _accessoryService.UpdateAccessory(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override MPSUpdateAccessoryResponse ErrorCheck(MPSUpdateAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new MPSUpdateAccessoryResponse() { Success = false };
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