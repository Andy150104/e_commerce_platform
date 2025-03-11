using Client.Controllers.V1.MomoPayment;
using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Models;
using Client.Models.Helper;
using Client.Services;
using Client.SystemClient;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NLog;

namespace Client.Controllers.V1.TOS;

/// <summary>
/// TOSInsertOrderController - Insert new Order
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class InsertOrderController : AbstractApiController<InsertOrderRequest, InsertOrderResponse, string>
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly IIdentityService _identityService;
    private readonly IOrderService _orderService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="identityApiClient"></param>
    public InsertOrderController(IIdentityService identityService, IIdentityApiClient identityApiClient, IOrderService orderService)
    {
        _identityService = identityService;
        _orderService = orderService;
        _identityApiClient = identityApiClient;
    }

    /// <summary>
    /// Incoming Post
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public override InsertOrderResponse ProcessRequest(InsertOrderRequest request)
    {
        return ProcessRequest(request, _identityService, logger, new InsertOrderResponse());
    }

    /// <summary>
    /// Main processing
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected override InsertOrderResponse Exec(InsertOrderRequest request)
    {
        return _orderService.InsertOrder(request, _identityService);
    }

    /// <summary>
    /// Error check
    /// </summary>
    /// <param name="request"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    protected internal override InsertOrderResponse ErrorCheck(InsertOrderRequest request, List<DetailError> detailErrorList)
    {
        var response = new InsertOrderResponse() { Success = false };

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