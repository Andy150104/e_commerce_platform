using Client.Controllers.V1.UDS;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Users;

/// <summary>
/// UDSUpdateUserProfileController - Update user profile
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSUpdateUserProfileController : AbstractApiController<UDSUpdateUserProfileRequest, UDSUpdateUserProfileReponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="userService"></param>
    public UDSUpdateUserProfileController(IIdentityApiClient identityApiClient, IIdentityService identityService, IUserService userService)
    {
        _identityService = identityService;
        _userService = userService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Put
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSUpdateUserProfileReponse ProcessRequest(UDSUpdateUserProfileRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new UDSUpdateUserProfileReponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override UDSUpdateUserProfileReponse Exec(UDSUpdateUserProfileRequest request)
    {
        return _userService.UpdateUserProfile(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override UDSUpdateUserProfileReponse ErrorCheck(UDSUpdateUserProfileRequest request, List<DetailError> detailErrorList)
    {
        var response = new UDSUpdateUserProfileReponse() { Success = false };

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