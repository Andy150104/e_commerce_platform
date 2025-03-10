using Client.Controllers.AbstractClass;
using Client.Controllers.V1.DPS;
using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Accessories;

/// <summary>
/// DPSSelectAccessoryController - Select Accessory
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectAccessoryController : AbstractApiGetControllerNotToken<DPSSelectAccessoryRequest, DPSSelectAccessoryResponse, DPSSelectAccessoryEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IAccessoryService _accessoryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="accessoryService"></param>
    public DPSSelectAccessoryController(IIdentityService identityService, IAccessoryService accessoryService)
    {
        _identityService = identityService;
        _accessoryService = accessoryService;
    }
    
    /// <summary>
    /// Incoming Post 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public override DPSSelectAccessoryResponse Get(DPSSelectAccessoryRequest request)
    {
        return Get(request, _identityService, logger, new DPSSelectAccessoryResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSSelectAccessoryResponse ExecGet(DPSSelectAccessoryRequest request)
    {
        return _accessoryService.SelectAccessory(request);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSSelectAccessoryResponse ErrorCheck(DPSSelectAccessoryRequest request, List<DetailError> detailErrorList) 
    {
        var response = new DPSSelectAccessoryResponse() { Success = false };
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