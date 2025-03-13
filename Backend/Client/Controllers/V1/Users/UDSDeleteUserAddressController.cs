using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Users;

/// <summary>
/// UDSDeleteUserAddressController - Delete user address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSDeleteUserAddressController : AbstractApiController<UDSDeleteUserAddressRequest, UDSDeleteUserAddressResponse, string>
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IAddressService _addressService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="addressService"></param>
    /// <param name="identityApiClient"></param>
    public UDSDeleteUserAddressController(IIdentityService identityService, IAddressService addressService, IIdentityApiClient identityApiClient)
    {
        _identityService = identityService;
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Patch
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSDeleteUserAddressResponse ProcessRequest(UDSDeleteUserAddressRequest request)
    {
        return ProcessRequest(request, _identityService, _logger, new UDSDeleteUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override UDSDeleteUserAddressResponse Exec(UDSDeleteUserAddressRequest request)
    {
        return _addressService.DeleteUserAddress(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override UDSDeleteUserAddressResponse ErrorCheck(UDSDeleteUserAddressRequest request, List<DetailError> detailErrorList)
    {
        var response = new UDSDeleteUserAddressResponse() { Success = false };

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