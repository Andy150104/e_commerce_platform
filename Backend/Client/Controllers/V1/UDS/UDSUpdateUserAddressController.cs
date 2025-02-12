using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.UPS;

/// <summary>
/// UDSUpdateUserAddressController - Update user address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSUpdateUserAddressController : AbstractApiController<UDSUpdateUserAddressRequest, UDSUpdateUserAddressResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    public UDSUpdateUserAddressController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override UDSUpdateUserAddressResponse Post(UDSUpdateUserAddressRequest request)
    {
        return Post(request, _context, logger, new UDSUpdateUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UDSUpdateUserAddressResponse Exec(UDSUpdateUserAddressRequest request, IDbContextTransaction transaction)
    {
        var response = new UDSUpdateUserAddressResponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get address
        var address = _context.Addresses.FirstOrDefault(x => x.Username == userName && x.AddressId == request.AddressId);
        
        // Update address information
        if (request.AddressLine != null)
        {
            address.AddressLine = request.AddressLine;
        }
        
        if (request.Ward != null)
        {
            address.Ward = request.Ward;
        }
        
        if (request.District != null)
        {
            address.District = request.District;
        }
        
        if (request.City != null)
        {
            address.City = request.City;
        }

        if (request.Province != null)
        {
            address.Province = request.Province;
        }
        
        // Save changes
        _context.Update(address);
        _context.SaveChanges(address.Username);
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
    protected internal override UDSUpdateUserAddressResponse ErrorCheck(UDSUpdateUserAddressRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UDSUpdateUserAddressResponse() { Success = false };

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