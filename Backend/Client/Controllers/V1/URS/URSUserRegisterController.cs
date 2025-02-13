using Client.Controllers;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Logics.Commons;
using server.Models;
using Server.Utils.Consts;

namespace server.Controllers.V1.UserRegisterScreen;

/// <summary>
/// UserRegisterController - Register new User
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class URSUserRegisterController : AbstractApiControllerNotToken<URSUserRegisterRequest, URSUserRegisterResponse, object>
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
        
        // Decrypt Key
        var keyDecrypt = CommonLogic.DecryptText(request.Key, _context);
        string[] values = keyDecrypt.Split(",");
        
        string userName = values[0];
        string email = values[1];
        string firstName = values[2];
        string lastName = values[3];
        
        // Check if the user already exists
        var userSelect = _context.VwUserAuthentications.AsNoTracking().FirstOrDefault(x => x.UserName == userName 
            || x.Email == email);
        
        // If the user already exists, return an error
        if (userSelect != null)
        {
            response.SetMessage(MessageId.E11004);
            return response;
        }
        
        // Check plan
        Guid? planExist =null;
        if (request.PlanId.HasValue && request.PlanId != Guid.Empty)
        { 
            var plan = _context.VwPlans.AsNoTracking().FirstOrDefault(x => x.PlanId == request.PlanId);
            if (plan == null)
            {
                response.SetMessage(MessageId.E00000, "Plan not found");
                return response;
            }
            planExist = plan.PlanId;
        }

        var newUser = new User()
        {
            UserName = userName,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            BirthDate = DateOnly.Parse(request.BirthDay),
            ImageUrl = request.ImageUrl,
            PlanId = planExist,
        };
        // Add new user
        _context.Add(newUser);

        // Update role
        var roleUpdate = new
        {
            UserName = newUser.UserName,
            PlanId = planExist,
        };

        // Call API to update role
        var httpClient = new HttpClient();
        var apiClient = new CommonLogic.ApiClient<URSUserRegisterResponse, object>(httpClient);
        var responseApi = apiClient.CallApiAsync<URSUserRegisterResponse>(
            HttpMethod.Post, 
            CommonUrl.Localhost5090UpdateRole,
            roleUpdate
        );

        if (responseApi.Result.Success == false)
        {
            response.SetMessage(responseApi.Result.MessageId);
            return response;
        }
        
        // Add address
        var newAddress = new Address()
        {
            Username = newUser.UserName,
            AddressLine = request.AddressLine,
            Ward = request.Ward,
            City = request.City,
            Province = request.Province,
            District = request.District,
        };
        _context.Add(newAddress);
        
        // Commit transaction
        _context.SaveChanges(newAddress.Username);
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