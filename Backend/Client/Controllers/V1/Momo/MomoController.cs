using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Models.Helper;
using Client.Services;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers.V1.MomoPayment
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly IMomoService _momoSerivce;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly AppDbContext _appDbContext;
        public MomoController(IMomoService momoSerivce, AppDbContext appDbContext, IOrderService orderService, IUserService userService)
        {
            _momoSerivce = momoSerivce;
            _appDbContext = appDbContext;
            _orderService = orderService;
            _userService = userService;
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
            const string trueUrl = "";
            const string falseUrl = "";
            var response = _momoSerivce.PaymentExecuteAsync(HttpContext.Request.Query);
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
                orderPlan.Status = (byte) ConstantEnum.OrderPlans.Success;
                orderPlan.Description = response.TransactionId;
                userName.PlanId = orderPlan.PlanId;
                userName.PlanExpired = DateTime.Now.AddMonths(plan.DurationMonths);
                _appDbContext.Users.Update(userName);
                _appDbContext.OrderPlans.Update(orderPlan);
                _appDbContext.SaveChanges();
                transaction.Commit();   
            }
            return Ok(true);
        }
        
        [HttpGet("Order")]
        public IActionResult PaymentOrderCallBack()
        {
            const string trueUrl = "http://localhost:3000/transaction/order?success=true";
            const string falseUrl = "";
            
            // Get response from Momo
            var response = _momoSerivce.PaymentExecuteAsync(HttpContext.Request.Query);
            
            // Get order
            var order = _orderService.Find(or => or.OrderId == Guid.Parse(response.OrderId)).FirstOrDefault()!;
            
            var user = _userService.Find(u => u.UserName == response.FullName).FirstOrDefault();
            if (user == null) 
                return BadRequest();
        
           _orderService.UpdateOrderStatus(order.OrderId, user.UserName);
           
           return Redirect(trueUrl);
        }
    }
}
