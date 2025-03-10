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
/// MPSInsertImageAccessoryController - Insert Image Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MSPInsertImageAccessoryController : AbstractApiAsyncController<MSPInsertImageAccessoryRequest, MSPInsertImageAccessoryResponse, string>
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
    public MSPInsertImageAccessoryController(IIdentityService identityService, IIdentityApiClient identityApiClient, IAccessoryService accessoryService)
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
    [HttpPost]
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<MSPInsertImageAccessoryResponse> Post(MSPInsertImageAccessoryRequest request)
    {
        return await Post(request, _identityService, logger, new MSPInsertImageAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<MSPInsertImageAccessoryResponse> Exec(MSPInsertImageAccessoryRequest request)
    {
        return await _accessoryService.InsertImageAccessory(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override MSPInsertImageAccessoryResponse ErrorCheck(MSPInsertImageAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new MSPInsertImageAccessoryResponse() { Success = false };
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