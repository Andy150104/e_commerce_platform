using Client.Controllers.AbstractClass;
using Client.Controllers.V1.UDS;
using Client.Models;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Client.Controllers.V1.Users;

/// <summary>
/// UDSSelectUserAddressController - Select user address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSSelectUserAddressController : AbstractApiGetController<UDSSelectUserAddressFilter, UDSSelectUserAddressResponse, List<UDSSelectUserAddressEntity>>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IAddressService _addressService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="addressService"></param>
    public UDSSelectUserAddressController(IIdentityApiClient identityApiClient, IIdentityService identityService,
        IAddressService addressService)
    {
        _identityApiClient = identityApiClient;
        _identityService = identityService;
        _addressService = addressService;
    }

    /// <summary>
    /// Incoming Get
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSSelectUserAddressResponse Get(UDSSelectUserAddressFilter filter)
    {
        return Get(filter, _identityService, _logger, new UDSSelectUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    protected override UDSSelectUserAddressResponse ExecGet(UDSSelectUserAddressFilter filter)
    {
        var response = new UDSSelectUserAddressResponse() { Success = false };

        // Get userName
        var userName = _identityService.IdentityEntity.UserName;

        // Get user address
        var userAddressSelect = _addressService
            .FindView(add => add.Username == userName)
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
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override UDSSelectUserAddressResponse ErrorCheck(UDSSelectUserAddressFilter request,List<DetailError> detailErrorList)
    {
        var response = new UDSSelectUserAddressResponse { Success = false };

        if (detailErrorList.Count > 0)
        {
            response.SetMessage(MessageId.E10000);
            response.DetailErrorList = detailErrorList;
            return response;
        }

        response.Success = true;
        return response;
    }
}