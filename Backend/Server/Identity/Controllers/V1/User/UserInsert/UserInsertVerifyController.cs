using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Server.Controllers;
using server.Logics.Commons;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace Server.Identity.Controllers.V1.User;

/// <summary>
/// UserInsertVerifyController - Verify the user registration
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserInsertVerifyController : AbstractApiControllerNotToken<UserInsertVerifyRequest, UserInsertVerifyResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    private readonly UserManager<Models.User> _userManager;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UserInsertVerifyController(AppDbContext context, UserManager<Models.User> userManager)
    {
        _context = context;
        _context._Logger = logger;
        _userManager = userManager;
    }
    
    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override UserInsertVerifyResponse Post(UserInsertVerifyRequest request)
    {
        return Post(request, _context, logger, new UserInsertVerifyResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UserInsertVerifyResponse Exec(UserInsertVerifyRequest request, IDbContextTransaction transaction)
    {
        var response = new UserInsertVerifyResponse() { Success = false };
        // Decrypt
        var userNameDecrypt = CommonLogic.DecryptText(request.Key, _context);
        // Check user exists
        var userExist = _context.VwUserVerifies.Any(x => x.UserName == userNameDecrypt);
        if (userExist == null)
        {
            response.SetMessage(MessageId.E11001);
            return response;
        }
        // Get information user
        var userVerify = _userManager.FindByNameAsync(userNameDecrypt).Result;
        
        // Update information user
        userVerify.IsActive = true;
        userVerify.LockoutEnd = null;
        userVerify.LockoutEnabled = false;
        userVerify.EmailConfirmed = true;
        
        var result = _userManager.UpdateAsync(userVerify).Result;
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
    protected internal override UserInsertVerifyResponse ErrorCheck(UserInsertVerifyRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UserInsertVerifyResponse() { Success = false };
        
        
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