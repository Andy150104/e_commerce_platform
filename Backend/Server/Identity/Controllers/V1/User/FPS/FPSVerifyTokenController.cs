using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using server.Logics.Commons;
using Server.Identity.Controllers.V1.User;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace Server.Controllers.V1.ForgetPasswordScreen
{
    /// <summary>
    /// FPSVerifyTokenController - Check validate OTP
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FPSVerifyTokenController : AbstractApiControllerNotToken<FPSVerifyTokenRequest, FPSVerifyTokenResponse, string>
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
        public override FPSVerifyTokenResponse Post(FPSVerifyTokenRequest request)
        {
            return Post(request, _context, logger, new FPSVerifyTokenResponse());
        }
        /// <summary>
        /// Main Processing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected override FPSVerifyTokenResponse Exec(FPSVerifyTokenRequest request, IDbContextTransaction transaction)
        {
            var response = new FPSVerifyTokenResponse() { Success = false };
            var detailErrorList = new List<DetailError>();
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
            if (key != request.OTP)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            // Find User
            var user = _userManager.FindByNameAsync(userNameDecrypt).Result;
            if (user == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            //Reset New Password
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
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
        /// <summary>
        /// Error Check
        /// </summary>
        /// <param name="request"></param>
        /// <param name="detailErrorList"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected internal override FPSVerifyTokenResponse ErrorCheck(FPSVerifyTokenRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
        {
            var response = new FPSVerifyTokenResponse() { Success = false };


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
}
