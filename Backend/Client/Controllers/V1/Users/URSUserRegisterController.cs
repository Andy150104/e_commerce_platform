using Client.Controllers.V1.UserRegisterScreen;
using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Users;

/// <summary>
/// UserRegisterController - Register new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class URSUserRegisterController : AbstractApiAsyncControllerNotToken<URSUserRegisterRequest, URSUserRegisterResponse, object>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="userService"></param>
    public URSUserRegisterController(IIdentityService identityService, IUserService userService)
    {
        _identityService = identityService;
        _userService = userService;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override async Task<URSUserRegisterResponse> ProcessRequest(URSUserRegisterRequest request)
    {
        return await ProcessRequest(request, _identityService, logger, new URSUserRegisterResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override async Task<URSUserRegisterResponse> Exec(URSUserRegisterRequest request)
    {
        return await _userService.Register(request);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override URSUserRegisterResponse ErrorCheck(URSUserRegisterRequest request, List<DetailError> detailErrorList)
    {
        var response = new URSUserRegisterResponse() { Success = false };

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