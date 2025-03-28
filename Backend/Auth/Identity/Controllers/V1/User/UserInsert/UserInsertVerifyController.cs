using Auth.Controllers;
using Auth.Models.Helper;
using Auth.Utils.Consts;
using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Auth.Logics.Commons;

namespace Auth.Identity.Controllers.V1.User;

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
    [HttpPost]
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
        var keyDecrypt = CommonLogic.DecryptText(request.Key, _context);
        string[] values = keyDecrypt.Split(",");
        string userNameDecrypt = values[0];
        
        // Check user exists
        var userExist = _context.VwUserVerifies.FirstOrDefault(x => x.UserName == userNameDecrypt);
        if (userExist == null)
        {
            response.SetMessage(MessageId.E11001);
            return response;
        }
        // Get information user
        var userVerify = _userManager.FindByNameAsync(userNameDecrypt).Result;
        
        // Update information user
        userVerify.Key = null;
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