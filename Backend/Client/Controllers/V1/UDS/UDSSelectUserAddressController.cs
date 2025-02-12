using Client.Models.Helper;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.UPS;

/// <summary>
/// UDSSelectUserAddressController - Select User Address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSSelectUserAddressController : AbstractApiController<UDSSelectUserAddressRequest, UDSSelectUserAddressResponse, List<UDSSelectUserAddressEntity>>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UDSSelectUserAddressController(AppDbContext context, IIdentityApiClient identityApiClient)
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
    public override UDSSelectUserAddressResponse Post(UDSSelectUserAddressRequest request)
    {
        return Post(request, _context, logger, new UDSSelectUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override UDSSelectUserAddressResponse Exec(UDSSelectUserAddressRequest request, IDbContextTransaction transaction)
    {
        var response = new UDSSelectUserAddressResponse() { Success = false };
        
        // Get userName
        var userName = _context.IdentityEntity.UserName;
        
        // Get user address
        var userAddressSelect = _context.VwUserAddresses
            .AsNoTracking()
            .Where(x => x.Username == userName)
            .Select(x => new UDSSelectUserAddressEntity
            {
                AddressId = x.AddressId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                AddressLine = x.AddressLine,
                Ward = x.Ward,
                District = x.District,
                City = x.City,
                Province = x.Province,
            })
            .ToList();
        
        // True
        response.Success = true;
        response.Response = userAddressSelect;
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
    protected internal override UDSSelectUserAddressResponse ErrorCheck(UDSSelectUserAddressRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
    {
        var response = new UDSSelectUserAddressResponse() { Success = false };

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