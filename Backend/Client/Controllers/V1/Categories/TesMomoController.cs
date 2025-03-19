// using Client.Controllers.AbstractClass;
// using Client.Controllers.V1.MomoPayment.MomoServices;
// using Client.Services;
// using Client.Utils.Consts;
// using Microsoft.AspNetCore.Mvc;
// using NLog;
//
// namespace Client.Controllers.V1.Categories;
//
// [Route("api/v1/Momo/Order")]
// [ApiController]
// public class TesMomoController : AbstractApiGetControllerNotToken<string, TesMomoResponse, IActionResult>
// {
//     private readonly IMomoService _momoSerivce;
//     private readonly IOrderService _orderService;
//     private readonly IUserService _userService;
//     private readonly IIdentityService _identityService;
//     private readonly Logger _logger = LogManager.GetCurrentClassLogger();
//
//     public TesMomoController(IMomoService momoSerivce, IOrderService orderService, IUserService userService, IIdentityService identityService)
//     {
//         _momoSerivce = momoSerivce;
//         _orderService = orderService;
//         _userService = userService;
//         _identityService = identityService;
//     }
//
//     public override TesMomoResponse Get(string filter)
//     {
//         return Get(filter, _identityService, _logger, new TesMomoResponse());
//     }
//
//     protected override TesMomoResponse ExecGet(string filter)
//     {
//         var res = new TesMomoResponse { Success = false };
//         const string trueUrl = "http://localhost:3000/transaction/order?success=true";
//         const string falseUrl = "";
//             
//         // Get response from Momo
//         var response = _momoSerivce.PaymentExecuteAsync(HttpContext.Request.Query);
//             
//         // Get order
//         var order = _orderService.Find(or => or.OrderId == Guid.Parse(response.OrderId)).FirstOrDefault()!;
//             
//         var user = _userService.Find(u => u.UserName == response.FullName).FirstOrDefault();
//         if (user == null)
//         {
//             res.SetMessage(MessageId.I00000, CommonMessages.);
//             res.Response = BadRequest();
//         }
//         
//         _orderService.UpdateOrderStatus(order.OrderId, user.UserName);
//            
//         return Redirect(trueUrl);
//     }
//
//     protected internal override TesMomoResponse ErrorCheck(string request, List<DetailError> detailErrorList)
//     {
//         throw new NotImplementedException();
//     }
// }