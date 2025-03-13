using Client.Controllers.V1.UDS;
using Client.Models;
using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.Users;

/// <summary>
/// UDSInsertUserAddressController - Insert user address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSInsertUserAddressController : AbstractApiController<UDSInsertUserAddressRequest, UDSInsertUserAddressResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IAddressService _addressService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="addressService"></param>
    public UDSInsertUserAddressController(IIdentityApiClient identityApiClient, IIdentityService identityService, IAddressService addressService)
    {
        _identityService = identityService;
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSInsertUserAddressResponse ProcessRequest(UDSInsertUserAddressRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new UDSInsertUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    protected override UDSInsertUserAddressResponse Exec(UDSInsertUserAddressRequest request)
    {
        return _addressService.InsertUserAddress(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override UDSInsertUserAddressResponse ErrorCheck(UDSInsertUserAddressRequest request, List<DetailError> detailErrorList)
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