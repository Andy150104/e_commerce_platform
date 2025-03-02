using Auth.Controllers;
using Auth.Models.Helper;
using Auth.Utils.Consts;
using Client.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Auth.SystemClient;

namespace Auth.Identity.Controllers;

/// <summary>
/// VerifyTokenController - Controller for verifying token
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class VerifyTokenController : AbstractApiController<VerifyTokenRequest, VerifyTokenResponse, VerifyTokenEntity>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public VerifyTokenController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }
    
    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override VerifyTokenResponse Post(VerifyTokenRequest request)
    {
        return Post(request, _context, logger, new VerifyTokenResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override VerifyTokenResponse Exec(VerifyTokenRequest request, IDbContextTransaction transaction)
    {
        var response = new VerifyTokenResponse() { Success = false };
        
        // Get role information
        var entityResponse = new VerifyTokenEntity
        {
            RoleName = _context.IdentityEntity.RoleName,
        };
        
        // True
        response.Success = true;
        response.Response = entityResponse;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override VerifyTokenResponse ErrorCheck(VerifyTokenRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new VerifyTokenResponse() { Success = false };

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