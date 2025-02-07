using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Models;

namespace server.Controllers.V1.UserRegisterScreen;

/// <summary>
/// UserRegisterController - Register new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class URSUserRegisterController : AbstractApiControllerNotToken<URSUserRegisterRequest, URSUserRegisterResponse, string>
{ 
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public URSUserRegisterController(AppDbContext context)
    {
        _context = context;
        _context._Logger = logger;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    public override URSUserRegisterResponse Post(URSUserRegisterRequest request)
    {
        return Post(request, _context, logger, new URSUserRegisterResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override URSUserRegisterResponse Exec(URSUserRegisterRequest request, IDbContextTransaction transaction)
    {
        var response = new URSUserRegisterResponse() { Success = false };

        // Check if the user already exists
        var userSelect = _context.VwUserAuthentications.AsNoTracking().FirstOrDefault(x => x.UserName == request.UserName 
                                                                             || x.Email == request.Email);
        
        // If the user already exists, return an error
        if (userSelect != null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }

        // Create a new user
        var newUser = new User()
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            BirthDate = DateOnly.Parse(request.BirthDay),
            FullName = request.FullName,
            ImageUrl = request.ImageUrl,
            PlanId = request.PlanId,
        };
        
        // Add new user
        _context.Add(newUser);
        _context.SaveChanges();
        transaction.Commit();
        
        // True
        response.Success = true;
        response.SetMessage(MessageId.I00001, "User registration");
        return response;
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected internal override URSUserRegisterResponse ErrorCheck(URSUserRegisterRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new URSUserRegisterResponse() { Success = false };

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