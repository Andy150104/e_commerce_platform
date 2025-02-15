using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.UPS;

/// <summary>
/// UDSSelectUserProfileController - Select user profile
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSSelectUserProfileController : AbstractApiController<UDSSelectUserProfileRequest, UDSSelectUserProfileResponse, UDSSelectUserProfileEntity>
{
    
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UDSSelectUserProfileController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override UDSSelectUserProfileResponse Post(UDSSelectUserProfileRequest request)
    {
        return Post(request, _context, logger, new UDSSelectUserProfileResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UDSSelectUserProfileResponse Exec(UDSSelectUserProfileRequest request, IDbContextTransaction transaction)
    {
        var response = new UDSSelectUserProfileResponse { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get user information
        var userSelect = _context.VwUserProfiles.AsNoTracking()
            .Where(x => x.UserName == userName)
            .Select(x => new UDSSelectUserProfileEntity
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ImageUrl = x.ImageUrl,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
            })
            .FirstOrDefault();
        
        // True
        response.Success = true;
        response.Response = userSelect;
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
    /// <exception cref="NotImplementedException"></exception>
    protected internal override UDSSelectUserProfileResponse ErrorCheck(UDSSelectUserProfileRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
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