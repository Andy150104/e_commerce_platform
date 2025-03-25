using Client.Logics.Commons.GHNLogics;
using Client.Logics.Commons.MomoLogics;
using Client.Models;
using Client.Models.Helper;
using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Utils.Consts;
using Client.Utils.Consts;
using SystemConfig = Client.Models.SystemConfig;

namespace Client.Controllers.V1.ThirdParty
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly IMomoService _momoService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IGHNLogic _ghnLogic;
        private readonly IBaseService<OrderDetail, Guid, VwOrderDetailsWithProduct> _orderDetailService;
        private readonly IBaseService<SystemConfig, string, VwSystemConfig> _systemConfigService;

        public MomoController(IMomoService momoService, AppDbContext appDbContext, IOrderService orderService,
            IUserService userService, IBaseService<OrderDetail, Guid, VwOrderDetailsWithProduct> orderDetailService,
            IEmailTemplateService emailTemplateService, IGHNLogic ghnLogic,
            IBaseService<SystemConfig, string, VwSystemConfig> systemConfigService)
        {
            _momoService = momoService;
            _appDbContext = appDbContext;
            _orderService = orderService;
            _userService = userService;
            _orderDetailService = orderDetailService;
            _emailTemplateService = emailTemplateService;
            _ghnLogic = ghnLogic;
            _systemConfigService = systemConfigService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreatePaymentMomo(MomoExecuteResponseModel model)
        //{
        //    var response = await _momoSerivce.CreatePaymentAsync(model);
        //    return Ok(response);    
        //}
        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            const string updateRole = "https://localhost:5090/api/v1/UpdateRole";
            const string trueUrl = "";
            const string falseUrl = "";
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            var orderPlan = _appDbContext.OrderPlans.FirstOrDefault(or => or.OrderId == Guid.Parse(response.OrderId));
            var plan = _appDbContext.Plans.AsNoTracking().FirstOrDefault(p => p.PlanId == orderPlan.PlanId);
            if (plan == null) return BadRequest();
            var userName = _appDbContext.Users.FirstOrDefault(u => u.UserName == response.FullName);
            if (userName == null) return BadRequest();
            if (response.ErrorCode != "0")
            {
                if (orderPlan != null)
                {
                    _appDbContext.OrderPlans.Remove(orderPlan);
                    _appDbContext.SaveChanges();
                    return BadRequest();
                }
            }

            using (var transaction = _appDbContext.Database.BeginTransaction())
            {
                orderPlan.Status = (byte)ConstantEnum.OrderPlans.Success;
                orderPlan.Description = response.TransactionId;
                userName.PlanId = orderPlan.PlanId;
                userName.PlanExpired = DateTime.Now.AddMonths(plan.DurationMonths);

                //Update Role
                var httpClient = new HttpClient();
                var body = new { isOnlyValidation = false, userName = userName.FirstName, planId = orderPlan.PlanId };
                var res = httpClient.PostAsJsonAsync(updateRole, body).Result;
                if (res.IsSuccessStatusCode)
                {
                    _appDbContext.Users.Update(userName);
                    _appDbContext.OrderPlans.Update(orderPlan);
                    _appDbContext.SaveChanges();
                    transaction.Commit();
                }
            }

            return Ok(true);
        }

        [HttpGet("Order")]
        public async Task<IActionResult> PaymentOrderCallBack()
        {
            // Get response from Momo
            var responseQuery = _momoService.PaymentExecuteOrderAsync(HttpContext.Request.Query);

            var extraValues = responseQuery.ExtraData.Split('|');
            
            var systemConfigs = await _systemConfigService.FindView().ToListAsync();
        
            // Get Url
            var orderWebRedirectFailUrl = systemConfigs.Find(s => s.Id == Client.Utils.Consts.SystemConfig.OrderWebRedirectFail)?.Value;
            var orderMobileRedirectFailUrl = systemConfigs.Find(s => s.Id == Client.Utils.Consts.SystemConfig.OrderMobileRedirectFail)?.Value;
            
            // PlatForm
            var platForm = extraValues[2];

            if (responseQuery.ErrorCode != ((byte) ConstantEnum.PaymentStatus.Success).ToString())
            {
                if (platForm == ((byte) ConstantEnum.Platform.Web).ToString())
                {
                    return Redirect(orderWebRedirectFailUrl!);
                }
                
                return Redirect(orderMobileRedirectFailUrl!);
            }

            // Get order
            var order = _orderService.Find(or => or.OrderId == Guid.Parse(responseQuery.OrderId)).FirstOrDefault()!;

            var user = _userService.Find(u => u.UserName == responseQuery.FullName,
                    true,
                    u => u.Addresses)
                .FirstOrDefault();
            if (user == null)
                return BadRequest();

            // Address
            var addressId = Guid.Parse(extraValues[1]);

            // Get Address
            var address = user.Addresses.FirstOrDefault(a => a.AddressId == addressId);

            // Get Order Details
            var orderDetails = _orderDetailService
                .Find(or => or.OrderId == order.OrderId,
                    true,
                    a => a.Accessory)
                .ToList();

            var listAccessoryName = new List<KeyValuePair<string, string>>();
            foreach (var orderDetail in orderDetails)
            {
                var accessory = orderDetail!.Accessory;
                listAccessoryName.Add(new KeyValuePair<string, string>(accessory.AccessoryId, accessory.Name));
            }

            // Create GHN order
            var ghn = await _ghnLogic.CreateOrderGHNAsync(orderDetails!, user, address!, listAccessoryName);
            var ghnCode = ghn.Data.OrderCode;
            
            // Get Url
            var orderWebRedirectSuccessUrl = systemConfigs.Find(s => s!.Id == Client.Utils.Consts.SystemConfig.OrderWebRedirectSuccess)?.Value;
            var orderMobileRedirectSuccessUrl = systemConfigs.Find(s => s!.Id == Client.Utils.Consts.SystemConfig.OrderMobileRedirectSuccess)?.Value;
            
            // Send Mail
            var detailErrorList = new List<DetailError>();
            MomoOrderSendMail.SendMailGhnCode(_emailTemplateService, order, user.Email!, _appDbContext, detailErrorList);

            if (platForm.Equals(((byte)ConstantEnum.Platform.Web).ToString()))
                return Redirect(ReplaceSuccessUrl(orderWebRedirectSuccessUrl!, order.OrderId.ToString(), ghnCode));
            else
                return Redirect(ReplaceSuccessUrl(orderMobileRedirectSuccessUrl!, order.OrderId.ToString(), ghnCode));
        }

        /// <summary>
        /// Replace OrderId and GhnCode for success URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="orderId"></param>
        /// <param name="ghnCode"></param>
        /// <returns></returns>
        private string ReplaceSuccessUrl(string url, string orderId, string ghnCode)
        {
            var replacements = new Dictionary<string, string>
            {
                { "${order_id}", orderId },
                { "${ghn_order_code}", ghnCode }
            };
            
            foreach (var replacement in replacements)
            {
                url = url.Replace(replacement.Key, replacement.Value);
            }
            
            return url;
        }
    }
}