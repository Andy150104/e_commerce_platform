// using Client.Controllers.V1.MomoPayment;
// using Client.Controllers.V1.MomoPayment.MomoServices;
// using Client.Models;
// using Client.Models.Helper;
// using Client.SystemClient;
// using Client.Utils.Consts;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore.Storage;
// using NLog;
//
// namespace Client.Controllers.V1.TOS;
//
// /// <summary>
// /// TOSInsertOrderController - Insert new Order
// /// </summary>
// [Route("api/v1/[controller]")]
// [ApiController]
// public class InsertOrderController : AbstractApiController<InsertOrderRequest, InsertOrderResponse, string>
// {
//     private static readonly Logger logger = LogManager.GetCurrentClassLogger();
//     private readonly AppDbContext _context;
//     private readonly IMomoService _momoService;
//
//     /// <summary>
//     /// Constructor
//     /// </summary>
//     /// <param name="context"></param>
//     /// <param name="identityApiClient"></param>
//     /// <param name="momoService"></param>
//     public InsertOrderController(AppDbContext context, IIdentityApiClient identityApiClient, IMomoService momoService)
//     {
//         _context = context;
//         _momoService = momoService;
//         _context._Logger = logger;
//         IdentityApiClient = identityApiClient;
//     }
//
//     /// <summary>
//     /// Incoming Post
//     /// </summary>
//     /// <param name="request"></param>
//     /// <returns></returns>
//     [HttpPost]
//     [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
//     public override InsertOrderResponse ProcessRequest(InsertOrderRequest request)
//     {
//         return ProcessRequest(request, _context, logger, new InsertOrderResponse());
//     }
//
//     /// <summary>
//     /// Main processing
//     /// </summary>
//     /// <param name="request"></param>
//     /// <param name="transaction"></param>
//     /// <returns></returns>
//     protected override InsertOrderResponse Exec(InsertOrderRequest request, IDbContextTransaction transaction)
//     {
//         var response = new InsertOrderResponse { Success = false };
//         
//         // Get userName
//         var userName = _context.IdentityEntity.UserName;
//         
//         var address = _context.VwUserAddresses.FirstOrDefault(a => a.AddressId == request.AddressId && a.Username == userName);
//         if (address == null)
//         {
//             response.SetMessage(MessageId.I00000, CommonMessages.AddressOfUserNotFound);
//             return response;
//         }
//         
//         // Insert new Order
//         var newOrder = new Order
//         {
//             OrderId = Guid.NewGuid(),
//             Username = userName,
//             Status = (byte) ConstantEnum.OrderStatus.Pending,
//             Quantity = request.OrderDetails.Count,
//         };
//
//         decimal totalPrice = 0;
//         foreach (var item in request.OrderDetails)
//         {
//             var accessory = _context.Accessories.FirstOrDefault(a => a.AccessoryId == item.AccessoryId);
//             if (accessory == null)
//             {
//                 response.SetMessage(MessageId.I00000, CommonMessages.ProductNotFound);
//                 return response;
//             }
//
//             if (item.Quantity > accessory.Quantity)
//             {
//                 response.SetMessage(MessageId.I00000, CommonMessages.QuantityIsGreaterStock);
//                 return response;
//             }
//             
//             // Insert new OrderDetail
//             var orderDetail = new OrderDetail
//             {
//                 OrderId = newOrder.OrderId,
//                 AccessoryId = accessory.AccessoryId,
//                 Quantity = item.Quantity,
//                 UnitPrice = accessory.Price * item.Quantity
//             };
//
//             newOrder.OrderDetails.Add(orderDetail);
//             totalPrice += orderDetail.UnitPrice;
//             accessory.Quantity -= item.Quantity;
//             _context.Accessories.Update(accessory);            
//         }
//
//         // Update TotalPrice of Order
//         newOrder.TotalPrice = totalPrice;
//         
//         // Save Changes
//         _context.Orders.Add(newOrder);
//         _context.SaveChanges(userName);
//         transaction.Commit();
//         
//         var res = _momoService.CreatePaymentAsync(new MomoExecuteResponseModel
//         {
//             FullName = $"{newOrder.OrderId}_{userName}",
//             Amount = Math.Round(totalPrice * 100, 0).ToString(),
//             OrderId = newOrder.OrderId.ToString(),
//             OrderInfo = $"{CommonMessages.OrderDescription}_{newOrder.OrderId}",
//         }).Result;
//         
//         // True
//         response.Success = true;
//         response.SetMessage(MessageId.I00001);
//         return response;
//     }
//
//     /// <summary>
//     /// Error check
//     /// </summary>
//     /// <param name="request"></param>
//     /// <param name="detailErrorList"></param>
//     /// <param name="transaction"></param>
//     /// <returns></returns>
//     protected internal override InsertOrderResponse ErrorCheck(InsertOrderRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
//     {
//         var response = new InsertOrderResponse() { Success = false };
//
//         if (detailErrorList.Count > 0)
//         {
//             // Error
//             response.SetMessage(MessageId.E10000);
//             response.DetailErrorList = detailErrorList;
//             return response;
//         }
//         // True
//         response.Success = true;
//         return response;
//     }
// }