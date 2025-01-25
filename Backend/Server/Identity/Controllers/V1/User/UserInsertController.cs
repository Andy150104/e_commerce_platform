using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Server.Models;
using Server.Models.Helper;
using Server.Utils.Consts;

namespace server.Identity.Controllers.V1.User;

/// <summary>
///  UserInsertController - Insert new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserInsertController : ControllerBase
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="context"></param>
    public UserInsertController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///  Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public UserInsertResponse Post(UserInsertRequest request)
    {
        var response = new UserInsertResponse() {Success = false};

        // Check user exists
        var userSelect = _context.Users.AsNoTracking().FirstOrDefault(x => x.UserName == request.Username || x.Email == request.Email);
                                                                                                        
        if (userSelect != null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }

        var user = new Server.Models.User()
        {
            UserName = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsEnabled = true,
            Email = request.Email,
            Role = new Role
            {
                Name = ConstantEnum.UserRole.Customer.ToString()
            }
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001);
        return response;
    }
}