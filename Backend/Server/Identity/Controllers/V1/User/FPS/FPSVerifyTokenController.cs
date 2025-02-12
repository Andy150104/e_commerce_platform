using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using server.Logics.Commons;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace Server.Controllers.V1.ForgetPasswordScreen
xuống hàng
{/// <summary>
/// FPSVerifyTokenController - Check validate OTP
/// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FPSVerifyTokenController : ControllerBase
    {
        xài class abstract 
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private UserManager<User> _userManager;
        đừng có viết tắt
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cache"></param>
        /// <param name="userManager"></param>
        public FPSVerifyTokenController(AppDbContext context, IMemoryCache cache, UserManager<User> userManager)
        {
            _context = context;
            _cache = cache;
            _context._Logger = logger;
            _userManager = userManager;
        }
        /// <summary>
        /// Main Processing
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FPSVerifyTokenResponse> Post(FPSVerifyTokenRequest request)
        {
            token truyền bằng header chứ sao truyền qua body
                
            var response = new FPSVerifyTokenResponse() { Success = false };
            var detailErrorList = new List<DetailError>();

dư khoảng trắng nhiều v
            // Decrypt
            var keyDecrypt = CommonLogic.DecryptText(request.OTP, _context);
            string[] values = keyDecrypt.Split(",");
            string userNameDecrypt = values[0];
            if (!_cache.TryGetValue(userNameDecrypt, out string key))
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            _cache.Remove(userNameDecrypt);
            if(key != request.OTP   )
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            // Find User
            var user = await _userManager.FindByNameAsync(userNameDecrypt);
            if (user == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            //Reset New Password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = _userManager.ResetPasswordAsync(user, token, request.NewPassWord);
            if (result == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            //true
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return response;
        }

    }
}
