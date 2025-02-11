using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace Server.Controllers.V1.ForgetPasswordScreen
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FPSVerifyTokenController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private UserManager<User> _userManager;

        public FPSVerifyTokenController(AppDbContext context, IMemoryCache cache, UserManager<User> userManager)
        {
            _context = context;
            _cache = cache;
            _context._Logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<FPSVerifyTokenResponse> Post(FPSVerifyTokenRequest request)
        {
            var response = new FPSVerifyTokenResponse() { Success = false};
            var detailErrorList = new List<DetailError>();

            if (request.Email != null)
            {
                if (!_cache.TryGetValue(request.Email, out string storeOTP) || storeOTP != request.OTP)
                {
                    response.SetMessage(MessageId.E11004);
                    return response;
                }
            }
            else if (request.UserName != null)
            {
                if(!_cache.TryGetValue(request.UserName, out string storeOTP) || storeOTP != request.OTP)
                {
                    response.SetMessage(MessageId.E11004);
                    return response;
                }
            }

            var userExist = await _userManager.FindByNameAsync(request.UserName)
                 ?? await _userManager.FindByEmailAsync(request.Email);
            if (userExist == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(userExist);
            var result = _userManager.ResetPasswordAsync(userExist, token,  request.NewPassWord);
            if(result == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            response.Success = true;
            response.SetMessage(MessageId.I00001);
            return response;
        }

    }
}
