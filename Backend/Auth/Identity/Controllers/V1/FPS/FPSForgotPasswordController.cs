using Auth.Identity.Controllers.V1.User;
using Auth.Models;
using Auth.Models.Helper;
using Auth.Utils.Consts;
using Client.Controllers;
using client.Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Auth.Logics.Commons;

namespace Auth.Controllers.V1.FPS;

/// <summary>
/// FPSForgotPasswordController - Forgot Password
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class FPSForgotPasswordController : AbstractApiAsyncControllerNotToken<FPSForgotPasswordRequest, FPSForgotPasswordResponse, string>
{
    private readonly AppDbContext _context;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private UserManager<User> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cache"></param>
    /// <param name="userManager"></param>
    public FPSForgotPasswordController(AppDbContext context, UserManager<User> userManager)
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
    /// <exception cref="NotImplementedException"></exception
    public override Task<FPSForgotPasswordResponse> Post(FPSForgotPasswordRequest request)
    {
        return Post(request, _context, logger, new FPSForgotPasswordResponse());
    }
    
    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override async Task<FPSForgotPasswordResponse> Exec(FPSForgotPasswordRequest request, IDbContextTransaction transaction)
    {
        var response = new FPSForgotPasswordResponse() { Success = false };
        var detailErrorList = new List<DetailError>();
        
        // Check user exists
        var userExist = await _userManager.FindByEmailAsync(request.Email);
        if (userExist == null)
        {
            response.SetMessage(MessageId.E11001);
            return response;
        }

        // Generating EncryptText 
        var key = CommonLogic.EncryptText(userExist.UserName, _context);
        userExist.Key = key;

        await _userManager.UpdateAsync(userExist);
        transaction.Commit();
        
        // SendMail
        FPSUpdatePasswordSendMail.SendMailOTPInformation(_context, userExist.UserName, request.Email, key, detailErrorList);
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(detailErrorList[0].MessageId, detailErrorList[0].ErrorMessage);
            logger.Error(response.Message);
            return response;
        }

        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }

    /// <summary>
    /// Error Check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override FPSForgotPasswordResponse ErrorCheck(FPSForgotPasswordRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new FPSForgotPasswordResponse() { Success = false };
        
        if (detailErrorList.Count > 0)
        {
            // Error
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
}



