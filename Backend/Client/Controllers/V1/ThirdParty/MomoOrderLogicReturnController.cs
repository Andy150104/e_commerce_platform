using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using OpenIddict.Validation.AspNetCore;

namespace Client.Controllers.V1.ThirdParty;

/// <summary>
/// MomoOrderLogicReturnController - Use for Momo after payment return
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class MomoOrderLogicReturnController : AbstractApiController<MomoOrderLogicReturnRequest, MomoOrderLogicReturnResponse, string>
{
    private readonly IIdentityService _identityService;
     private readonly IOrderService _orderService;
     private readonly Logger _logger = LogManager.GetCurrentClassLogger();

     public MomoOrderLogicReturnController(IIdentityService identityService, IOrderService orderService)
     {
         _identityService = identityService;
         _orderService = orderService;
     }

    
     [HttpPatch]
     [Authorize(Roles = ConstRole.Owner, AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
     public override MomoOrderLogicReturnResponse ProcessRequest(MomoOrderLogicReturnRequest request)
     {
         return ProcessRequest(request, _identityService, _logger, new MomoOrderLogicReturnResponse());
     }

     protected override MomoOrderLogicReturnResponse Exec(MomoOrderLogicReturnRequest request)
     {
         return _orderService.UpdateOrderStatusBySystem(request, _identityService);
     }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="request"></param>
     /// <param name="detailErrorList"></param>
     /// <returns></returns>
     /// <exception cref="NotImplementedException"></exception>
     protected internal override MomoOrderLogicReturnResponse ErrorCheck(MomoOrderLogicReturnRequest request, List<DetailError> detailErrorList)
     {
         throw new NotImplementedException();
     }
}