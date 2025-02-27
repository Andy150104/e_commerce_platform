using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Models.Helper;
using Client.Utils.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers.V1.MomoPayment
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private IMomoService _momoSerivce;
        private readonly AppDbContext _appDbContext;
        public MomoController(IMomoService momoSerivce, AppDbContext appDbContext)
        {
            _momoSerivce = momoSerivce;
            _appDbContext = appDbContext;
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
    }
}
