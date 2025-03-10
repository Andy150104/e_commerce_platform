// using Client.Models;
// using Client.Models.Helper;
// using Client.SystemClient;
// using Client.Utils.Consts;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Storage;
// using NLog;
//
// namespace Client.Controllers.V1.TOS;
//
// /// <summary>
// /// TOSSelectOrdersController - Select Orders
// /// </summary>
// [Route("api/v1/[controller]")]
// [ApiController]
// public class SelectOrdersController : AbstractApiController<SelectOrdersRequest, SelectOrdersResponse, List<SelectOrdersEntity>>
// {
//     private static readonly Logger logger = LogManager.GetCurrentClassLogger();
//     private readonly AppDbContext _context;
//
//     /// <summary>
//     /// Constructor
//     /// </summary>
//     /// <param name="context"></param>
//     /// <param name="identityApiClient"></param>
//     public SelectOrdersController(AppDbContext context, IIdentityApiClient identityApiClient)
//     {
//         _context = context;
//         _context._Logger = logger;
//         IdentityApiClient = identityApiClient;
//     }
//
//     /// <summary>
//     /// Incoming Post
//     /// </summary>
//     /// <param name="request"></param>
//     /// <returns></returns>
//     public override SelectOrdersResponse ProcessRequest(SelectOrdersRequest request)
//     {
//         return ProcessRequest(request, _context, logger, new SelectOrdersResponse());
//     }
//
//     protected override SelectOrdersResponse Exec(SelectOrdersRequest request, IDbContextTransaction transaction)
// {
//     var response = new SelectOrdersResponse { Success = false };
//     var responseOrder = new List<SelectOrdersEntity>();
//
//     // Get userName
//     var userName = _context.IdentityEntity.UserName;
//
//     // Select Orders
//     List<VwOrder> orderSelects = _context.VwOrders
//         .AsNoTracking()
//         .Where(order => order.Username == userName && 
//                         (!request.OrderId.HasValue || order.OrderId == request.OrderId))
//         .ToList();
//
//     // Response
//     foreach (var order in orderSelects)
//     {
//         var orderDetails = _context.VwOrderDetailsWithProducts
//             .AsNoTracking()
//             .Where(orderDetail => orderDetail.OrderId == order.OrderId) // Thay vì request.OrderId
//             .Select(x => new TOSSelectOrderDetails
//             {
//                 OrderDetailId = x.OrderDetailId,
//                 AccessoryId = x.AccessoryId,
//                 ProductName = x.ProductName,
//                 Quantity = x.Quantity,
//                 OriginalPrice = x.OriginalPrice,
//                 UnitPrice = x.UnitPrice,
//                 DiscountPercent = x.DiscountPercent,
//                 DiscountedPrice = x.DiscountedPrice,
//                 CreatedAt = x.CreatedAt,
//                 CreatedBy = x.CreatedBy,
//                 UpdatedAt = x.UpdatedAt,
//                 UpdatedBy = x.UpdatedBy,
//                 ProductDescription = x.ProductDescription
//             })
//             .ToList();
//
//         // Add Order
//         responseOrder.Add(new SelectOrdersEntity
//         {
//             OrderId = order.OrderId,
//             Username = order.Username,
//             Quantity = order.Quantity,
//             TotalPrice = order.TotalPrice,
//             CreatedAt = order.CreatedAt,
//             UpdatedAt = order.UpdatedAt,
//             UpdatedBy = order.UpdatedBy,
//             Status = order.OrderStatus,
//             OrderDetails = orderDetails
//         });
//     }
//
//     // True
//     response.Success = true;
//     response.Response = responseOrder;
//     response.SetMessage(MessageId.I00001);
//     return response;
// }
//
//
//     /// <summary>
//     /// Error Check
//     /// </summary>
//     /// <param name="request"></param>
//     /// <param name="detailErrorList"></param>
//     /// <param name="transaction"></param>
//     /// <returns></returns>
//     protected internal override SelectOrdersResponse ErrorCheck(SelectOrdersRequest request, List<DetailError> detailErrorList, IDbContextTransaction transaction)
//     {
//         var response = new SelectOrdersResponse() { Success = false };
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