using Client.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Server.Controllers;
using server.Logics.Commons;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace client.Identity.Controllers.V1.FPS;

/// <summary>
/// FPSVerifyKeyController - Verify the key
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class FPSVerifyKeyController: AbstractApiAsyncControllerNotToken<FPSVerifyKeyRequest, FPSVerifyKeyResponse, string>
    {
        private readonly AppDbContext _context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private UserManager<Server.Models.User> _userManager;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cache"></param>
        /// <param name="userManager"></param>
        public FPSVerifyKeyController(AppDbContext context, UserManager<Server.Models.User> userManager)
        {
            _context = context;
            _context._Logger = logger;
            _userManager = userManager;
        }
        
        /// <summary>
        /// Main Processing
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override Task<FPSVerifyKeyResponse> Post(FPSVerifyKeyRequest request)
        {
            return Post(request, _context, logger, new FPSVerifyKeyResponse());
        }
        
        /// <summary>
        /// Main Processing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected override async Task<FPSVerifyKeyResponse> Exec(FPSVerifyKeyRequest request, IDbContextTransaction transaction)
        {
            var response = new FPSVerifyKeyResponse() { Success = false };

        var encodedKey = Uri.UnescapeDataString(request.Key);

        // Decrypt
        var userName = CommonLogic.DecryptText(encodedKey, _context);
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            // Key Check
            if (user.Key == null)
            {
                response.SetMessage(MessageId.E00000, "Key is invalid");
                return response;
            }
            user.Key = null;
            
            // Reset Password
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassWord);
            if (!result.Succeeded)
            {
                response.SetMessage(MessageId.E11005);
                return response;
            }
            
            // Update
            await _userManager.UpdateAsync(user);
            transaction.Commit();

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
        protected internal override FPSVerifyKeyResponse ErrorCheck(FPSVerifyKeyRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
        {
            var response = new FPSVerifyKeyResponse() { Success = false };
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