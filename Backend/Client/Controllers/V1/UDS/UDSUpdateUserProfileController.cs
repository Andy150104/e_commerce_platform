using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.UPS;

/// <summary>
/// UDSUpdateUserProfileController - Update user profile
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSUpdateUserProfileController : AbstractApiController<UDSUpdateUserProfileRequest, UDSUpdateUserProfileReponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UDSUpdateUserProfileController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override UDSUpdateUserProfileReponse Post(UDSUpdateUserProfileRequest request)
    {
        return Post(request, _context, logger, new UDSUpdateUserProfileReponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UDSUpdateUserProfileReponse Exec(UDSUpdateUserProfileRequest request, IDbContextTransaction transaction)
    {
        var response = new UDSUpdateUserProfileReponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get user
        var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
        
        // Update user information
        if (request.LastName != null && request.FirstName != null)
        {
            user.FullName = request.LastName + " " + request.FirstName;
        }

        if (request.PhoneNumber != null)
        {
            user.PhoneNumber = request.PhoneNumber;
        }

        if (request.ImageUrl != null)
        {
            user.ImageUrl = request.ImageUrl;
        }

        if (request.BirthDay != null)
        {
            user.BirthDate = DateOnly.Parse(request.BirthDay);
        }

        if (request.Gender != null)
        {
            user.Gender = request.Gender;
        }

        // Update
        _context.Users.Update(user);
        _context.SaveChanges(user.UserName);
        transaction.Commit();
        
        // True
        response.Success = true;
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
    protected internal override UDSUpdateUserProfileReponse ErrorCheck(UDSUpdateUserProfileRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
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