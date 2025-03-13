using Client.Controllers.AbstractClass;
using Client.Controllers.V1.UDS;
using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Users;

/// <summary>
/// UDSSelectUserProfileController - Select user profile
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSSelectUserProfileController : AbstractApiGetController<UDSSelectUserProfileRequest, UDSSelectUserProfileResponse, UDSSelectUserProfileEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="userService"></param>
    public UDSSelectUserProfileController(AppDbContext context, IIdentityApiClient identityApiClient, IIdentityService identityService, IUserService userService)
    {
        _identityService = identityService;
        _userService = userService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSSelectUserProfileResponse Get(UDSSelectUserProfileRequest request)
    {
        return Get(request, _identityService, logger, new UDSSelectUserProfileResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override UDSSelectUserProfileResponse ExecGet(UDSSelectUserProfileRequest request)
    {
        return _userService.SelectUserProfile(_identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    protected internal override UDSSelectUserProfileResponse ErrorCheck(UDSSelectUserProfileRequest request, List<DetailError> detailErrorList)
    {
        var response = new UDSSelectUserProfileResponse() { Success = false };

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