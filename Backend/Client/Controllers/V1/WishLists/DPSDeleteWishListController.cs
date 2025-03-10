using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSDeleteWishListController - Delete wishlist item
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSDeleteWishListController : AbstractApiController<DPSDeleteWishListRequest, DPSDeleteWishListResponse, string>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IWishListService _service;
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="service"></param>
    /// <param name="identityService"></param>
    public DPSDeleteWishListController(IIdentityApiClient identityApiClient, IWishListService service, IIdentityService identityService)
    {
        _service = service;
        _identityService = identityService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSDeleteWishListResponse ProcessRequest(DPSDeleteWishListRequest request)
    {
        return ProcessRequest(request, _identityService, _logger, new DPSDeleteWishListResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSDeleteWishListResponse Exec(DPSDeleteWishListRequest request)
    {
        return _service.DeleteWishList(request, _identityService);
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSDeleteWishListResponse ErrorCheck(DPSDeleteWishListRequest request, List<DetailError> detailErrorList)
    {
        var response = new DPSDeleteWishListResponse() { Success = false };
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