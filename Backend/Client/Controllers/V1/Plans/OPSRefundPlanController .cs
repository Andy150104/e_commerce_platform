// using Client.Controllers.V1.MomoPayment;
// using Client.Controllers.V1.MomoPayment.MomoServices;
// using Client.Controllers.V1.OPS;
// using Client.Models.Helper;
// using Client.SystemClient;
// using Client.Utils.Consts;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using NLog;
//
// namespace Client.Controllers.V1.Plans;
//
// /// <summary>
// /// OPSRefundPlanController - Buying plan
// /// </summary>
// [Route("api/v1/[controller]")]
// [ApiController]
// public class OpsRefundPlanController : AbstractApiController<OPSRefundPlanRequest, OPSRefundPlanResponse, string>
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
//     public OpsRefundPlanController(AppDbContext context, IIdentityApiClient identityApiClient, IMomoService momoService)
//     {
//         _context = context;
//         _context._Logger = logger;
//         _identityApiClient = identityApiClient;
//         _momoService = momoService;
//     }
//     
//     /// <summary>
//     /// Incoming Post
//     /// </summary>
//     /// <param name="request"></param>
//     /// <returns></returns
//     [HttpPost]
//     [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
//     public override OPSRefundPlanResponse ProcessRequest(OPSRefundPlanRequest request)
//     {
//         return ProcessRequest(request, _context, logger, new OPSRefundPlanResponse());
//     }
//
//     /// <summary>
//     /// Main processing
//     /// </summary>
//     /// <param name="request"></param>
//     /// <returns></returns>
//     protected override OPSRefundPlanResponse Exec(OPSRefundPlanRequest request)
//     {
//         var response = new OPSRefundPlanResponse() { Success = false };
//         
//         // Get userName
//         var username = _context.IdentityEntity.UserName;
//         
//         // Get RefundRequest
//         var refundRequest = _context.RefundPlanRequests.FirstOrDefault(rp => rp.RefundRequests == request.RefundRequestId);
//         if (refundRequest == null)
//         {
//             response.SetMessage(MessageId.E11004);
//             return response;
//         }
//         
//         // Get OrderPlan
//         var orderPlan = _context.OrderPlans.FirstOrDefault(op => op.OrderId == refundRequest.OrderPlanId);
//         if (orderPlan == null)
//         {
//             response.SetMessage(MessageId.E11004);
//             return response;
//         }
//         
//         // Create Refund
//         var refundResponse = _momoService.CreateRefundAsync(new MomoRefundRequest
//         {
//             OrderId = "",
//             Amount = long.Parse(Math.Round(orderPlan.Price * 100, 0).ToString()),
//             Description = refundRequest.Reason,
//             Lang = "vi",
//             RequestId = Guid.NewGuid().ToString(),
//             PartnerCode = "MOMO",
//             TransId = long.Parse(orderPlan.Description)
//
//         }).Result;
//         
//         // Update RefundRequest
//         refundRequest.ResultCode = refundResponse.ResultCode;
//         refundRequest.ResultResponse = refundResponse.Message;
//         if (refundResponse.ResultCode == 0)
//         {   
//             refundRequest.Status = (byte)RefundRequestEnum.Accepted;
//             orderPlan.Status = (byte) ConstantEnum.OrderPlans.Refunded;
//             _context.OrderPlans.Update(orderPlan);
//         }
//         else
//         {
//             refundRequest.Status = (byte)RefundRequestEnum.Denied;
//             _context.RefundPlanRequests.Update(refundRequest);
//             _context.SaveChanges(username);
//             transaction.Commit();
//             response.SetMessage(refundResponse.Message);
//             return response;
//         }
//         
//         // Update RefundRequest
//         _context.RefundPlanRequests.Update(refundRequest);
//         _context.SaveChanges(username);
//         transaction.Commit();
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
//     /// <param name="detailerrorlist"></param>
//     /// <returns></returns>
//     protected internal override OPSRefundPlanResponse ErrorCheck(OPSRefundPlanRequest request, List<DetailError> detailerrorlist)
//     {
//         var response = new OPSRefundPlanResponse() { Success = false };
//         if (detailerrorlist.Count > 0)
//         {
//             response.SetMessage(MessageId.E10000);
//             response.DetailErrorList = detailerrorlist;
//             return response;
//         }
//         
//         // True
//         response.Success = true;
//         return response;
//     }
// }
//
