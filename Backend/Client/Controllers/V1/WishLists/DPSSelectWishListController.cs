using Client.Controllers.AbstractClass;
using Client.Models;
using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.DPS;

/// <summary>
/// DPSSelectWishListController - Select WishList
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class DPSSelectWishListController : AbstractApiGetController<DPSSelectWishListRequest, DPSSelectWishListResponse, List<DPSSelectWishListEntity>>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IWishListService _wishListService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="wishListService"></param>
    public DPSSelectWishListController(IIdentityApiClient identityApiClient, IIdentityService identityService, IWishListService wishListService)
    {
        _identityService = identityService;
        _wishListService = wishListService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="itemRequest"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override DPSSelectWishListResponse Get(DPSSelectWishListRequest itemRequest)
    {
        return Get(itemRequest, _identityService , _logger, new DPSSelectWishListResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override DPSSelectWishListResponse ExecGet(DPSSelectWishListRequest request)
    {
        return _wishListService.SelectWishList(_identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override DPSSelectWishListResponse ErrorCheck(DPSSelectWishListRequest request, List<DetailError> detailErrorList)
    {
        var response = new DPSSelectWishListResponse() { Success = false };
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