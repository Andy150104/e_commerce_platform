using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.UDS;

/// <summary>
/// UDSUpdateUserAddressController - Update user address
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UDSUpdateUserAddressController : AbstractApiController<UDSUpdateUserAddressRequest, UDSUpdateUserAddressResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IAddressService _addressService;
    private readonly IIdentityService _identityService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="identityApiClient"></param>
    /// <param name="identityService"></param>
    /// <param name="addressService"></param>
    public UDSUpdateUserAddressController(IIdentityApiClient identityApiClient, IIdentityService identityService, IAddressService addressService)
    {
        _identityService = identityService;
        _addressService = addressService;
        _identityApiClient = identityApiClient;
    }
    
    /// <summary>
    /// Incoming Put
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override UDSUpdateUserAddressResponse ProcessRequest(UDSUpdateUserAddressRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new UDSUpdateUserAddressResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override UDSUpdateUserAddressResponse Exec(UDSUpdateUserAddressRequest request)
    {
       return _addressService.UpdateUserAddress(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override UDSUpdateUserAddressResponse ErrorCheck(UDSUpdateUserAddressRequest request, List<DetailError> detailErrorList)
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