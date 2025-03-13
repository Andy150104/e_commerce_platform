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
/// MPSInsertAccessoryController - Insert Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class MPSInsertAccessoryController : AbstractApiAsyncController<MPSInsertAccessoryRequest, MPSInsertAccessoryResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger(); 
    private readonly IIdentityService _identityService;
    private readonly IAccessoryService _accessoryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="accessoryService"></param>
    public MPSInsertAccessoryController(IIdentityApiClient identityApiClient, IIdentityService identityService, IAccessoryService accessoryService)
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
    [Authorize(Roles = ConstRole.SaleEmployee + "," + ConstRole.Owner, 
        AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override async Task<MPSInsertAccessoryResponse> Post(MPSInsertAccessoryRequest request)
    {
        return await Post(request, _identityService, logger, new MPSInsertAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<MPSInsertAccessoryResponse> Exec(MPSInsertAccessoryRequest request)
    {
        return await _accessoryService.InsertAccessory(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override MPSInsertAccessoryResponse ErrorCheck(MPSInsertAccessoryRequest request, List<DetailError> detailErrorList)
    {
        var response = new MPSInsertAccessoryResponse() { Success = false };
        
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