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
public class FPSUpdatePasswordController : ControllerBase
{
    sử dụng abstract controller
    stash code lại r mr main qua tui mới thêm class abstract 
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private UserManager<User> _userManager;

    /// <summary>
    /// ctor
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

    dư space
    /// <summary>
    ///    Main Processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<FPSUpdatePasswordResponse> Post(FPSUpdatePasswordRequest request)
    {
        var response = new FPSUpdatePasswordResponse() { Success = false };
        var detailErrorList = new List<DetailError>();
        using (var transaction = _context.Database.BeginTransaction())
        {
            // Check user exists
            var userExist = await _userManager.FindByEmailAsync(request.Email);
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
    }
}


