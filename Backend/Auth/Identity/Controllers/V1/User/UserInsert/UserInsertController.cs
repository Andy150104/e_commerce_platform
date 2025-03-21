using Auth.Controllers;
using Auth.Models;
using Auth.Models.Helper;
using Auth.Utils.Consts;
using client.Identity.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using Auth.Logics.Commons;

namespace Auth.Identity.Controllers.V1.User;

/// <summary>
/// UserInsertController - Insert new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserInsertController : AbstractApiAsyncControllerNotToken<UserInsertRequest, UserInsertResponse, string>
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
    /// <param name="roleManager"></param>
    public UserInsertController(AppDbContext context, UserManager<Models.User> userManager, RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public override Task<UserInsertResponse> Post(UserInsertRequest request)
    {
        return Post(request, _context, logger, new UserInsertResponse());
    }
    
    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected override async Task<UserInsertResponse> Exec(UserInsertRequest request, IDbContextTransaction transaction)
    {
        var response = new UserInsertResponse() { Success = false };
        var detailErrorList = new List<DetailError>();
        
        var customerRole = ConstantEnum.UserRole.Customer.ToString();
        
        // Check user exists
        var userExist = await _userManager.FindByNameAsync(request.Username) 
                        ?? await _userManager.FindByEmailAsync(request.Email);
        
        // If user exists
        if (userExist != null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        
        // Create key
        var key = $"{request.Username},{request.Email},{request.FirstName},{request.LastName}";
        key = CommonLogic.EncryptText(key, _context);
        
        // Check role
        var role = await _roleManager.FindByNameAsync(customerRole);
        
        // Insert information user
        var user = new Models.User
        {
            UserName = request.Username,
            Email = request.Email,
            LockoutEnabled = true,
            LockoutEnd = null,
            EmailConfirmed = false,
            Key = key,
            RoleId = role.Id,
        };
        
        // Create user
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            response.SetMessage(MessageId.E11005);
            return response;
        }
        await _userManager.AddToRoleAsync(user, customerRole);
            
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

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    protected internal override UserInsertResponse ErrorCheck(UserInsertRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UserInsertResponse() { Success = false };
        
        // Validate password
        var passwordValidationService = new PasswordValidationService(_userManager);
        passwordValidationService.ValidatePasswordAsync(request.Password, detailErrorList);
        
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

/// <summary>
/// PasswordValidationService - Validate password
/// </summary>
public class PasswordValidationService
{
    private readonly UserManager<Models.User> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userManager"></param>
    public PasswordValidationService(UserManager<Models.User> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Validate password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<List<DetailError>> ValidatePasswordAsync(string password, List<DetailError> detailErrorList)
    {
        var user = new Models.User();
        var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();

        // Check password
        if (passwordValidator == null)
        {
            throw new InvalidOperationException("No password validator found.");
        }

        // Validate password
        var result = await passwordValidator.ValidateAsync(_userManager, user, password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                detailErrorList.Add(new DetailError
                {
                    field = "Password",
                    MessageId = MessageId.E11005,
                    ErrorMessage = error.Description
                });
            }
        }

        return detailErrorList;
    }
}
