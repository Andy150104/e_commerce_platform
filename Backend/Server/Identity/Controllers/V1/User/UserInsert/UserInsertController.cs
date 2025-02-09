using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Server.Controllers;
using server.Logics.Commons;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace Server.Identity.Controllers.V1.User;

/// <summary>
///  UserInsertController - Insert new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserInsertController : ControllerBase
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    private readonly UserManager<Models.User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public UserInsertController(AppDbContext context, UserManager<Models.User> userManager, RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    /// <summary>
    ///  Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserInsertResponse> Post(UserInsertRequest request)
    {
        var response = new UserInsertResponse() {Success = false};
        var detailErrorList = new List<DetailError>();

        using (var transaction = _context.Database.BeginTransaction())
        {
            // Check user exists
            var userExist = await _userManager.FindByNameAsync(request.Username) 
                            ?? await _userManager.FindByEmailAsync(request.Email);
            if (userExist != null)
            {
                response.SetMessage(MessageId.E11004);
                return response;
            }
            // Check role
            var role = await _roleManager.FindByNameAsync(ConstantEnum.UserRole.CUSTOMER.ToString());
            // Insert information user
            var user = new Models.User
            {
                UserName = request.Username,
                Email = request.Email,
                LockoutEnabled = true,
                LockoutEnd = null,
                EmailConfirmed = false,
                RoleId = role.Id,
            };
        
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.SetMessage(MessageId.E11005);
                return response;
            }
            await _userManager.AddToRoleAsync(user, ConstantEnum.UserRole.CUSTOMER.ToString());

            // Create key
            var key = $"{request.Username},{request.Email},{request.FirstName},{request.LastName}";
            key = CommonLogic.EncryptText(key, _context);
            
            // Send mail
            UserInsertSendMail.SendMailVerifyInformation(_context, user.UserName, user.Email, key, detailErrorList);
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