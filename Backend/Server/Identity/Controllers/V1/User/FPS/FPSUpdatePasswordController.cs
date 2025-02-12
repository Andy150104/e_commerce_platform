using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using server.Logics.Commons;
using Server.Identity.Controllers.V1.User;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers.V1.ForgetPasswordScreen;
/// <summary>
/// FPSUpdatePasswordController - Forget Password Function 
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class FPSUpdatePasswordController : AbstractApiControllerNotToken<FPSUpdatePasswordRequest, FPSUpdatePasswordResponse, string>
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private UserManager<User> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cache"></param>
    /// <param name="userManager"></param>
    public FPSUpdatePasswordController(AppDbContext context, IMemoryCache cache, UserManager<User> userManager)
    {
        _context = context;
        _cache = cache;
        _context._Logger = logger;
        _userManager = userManager;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>    
    /// <exception cref="NotImplementedException"></exception
    public override FPSUpdatePasswordResponse Post(FPSUpdatePasswordRequest request)
    {
        return Post(request, _context, logger, new FPSUpdatePasswordResponse());
    }


    /// <summary>
    /// Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override FPSUpdatePasswordResponse Exec(FPSUpdatePasswordRequest request, IDbContextTransaction transaction)
    {
        var response = new FPSUpdatePasswordResponse() { Success = false };
        var detailErrorList = new List<DetailError>();
        // Check user exists
        var userExist = _userManager.FindByEmailAsync(request.Email).Result;
        if (userExist == null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }

        // Generating EncryptText 
        var key = $"{userExist.UserName},{userExist.Email},{userExist.Id}";
        key = CommonLogic.EncryptText(key, _context);

        //Add cache 1min
        _cache.Set(userExist.UserName!, key, TimeSpan.FromMinutes(1));

        // SendMail
        var result = FPSUpdatePasswordSendMail.SendMailOTPInformation(_context, userExist.UserName!, request.Email!, key, detailErrorList);
        if (detailErrorList.Count > 0)
        {
            response.SetMessage(detailErrorList[0].MessageId, detailErrorList[0].ErrorMessage);
            logger.Error(response.Message);
            transaction.Rollback();
            return response;
        }

        // True
        transaction.Commit();
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
    protected internal override FPSUpdatePasswordResponse ErrorCheck(FPSUpdatePasswordRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new FPSUpdatePasswordResponse() { Success = false };


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


