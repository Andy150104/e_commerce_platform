using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;
using server.Models;

namespace Client.Controllers.V1.UPS;

/// <summary>
/// UDSInsertUserAddressController - Insert user address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSInsertUserAddressController : AbstractApiController<UDSInsertUserAddressRequest, UDSInsertUserAddressResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UDSInsertUserAddressController(AppDbContext context, IIdentityApiClient identityApiClient)
    {
        _context = context;
        _context._Logger = logger;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSInsertUserAddressResponse Post(UDSInsertUserAddressRequest request)
    {
        return Post(request, _context, logger, new UDSInsertUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UDSInsertUserAddressResponse Exec(UDSInsertUserAddressRequest request, IDbContextTransaction transaction)
    {
        var response = new UDSInsertUserAddressResponse() { Success = false };

        // Get userName
        var userName = _context.IdentityEntity.UserName;

        // Insert user address
        var userAddress = new Address
        {
            Username = userName,
            AddressLine = request.AddressLine,
            Ward = request.Ward,
            District = request.District,
            City = request.City,
            Country = request.Country
        };
        _context.Addresses.Add(userAddress);
        _context.SaveChanges(userAddress.Username);
        transaction.Commit();

        // True
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
    /// <exception cref="NotImplementedException"></exception>
    protected internal override UDSInsertUserAddressResponse ErrorCheck(UDSInsertUserAddressRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UDSInsertUserAddressResponse() { Success = false };

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