using Client.Controllers.V1.MomoPayment.MomoServices;
using Client.Models.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.V1.MomoServices
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
            var check = HttpContext.Request.Query;
            var response = _momoSerivce.PaymentExecuteAsync(HttpContext.Request.Query);
            var orderPlan = _appDbContext.OrderPlans.FirstOrDefault(or => or.OrderId == Guid.Parse(response.OrderId));
            if (check["resultCode"] != 0)
            { 
                
            }
               
            using (var transaction = _appDbContext.Database.BeginTransaction())
            {
            }
            return Ok(response);
        }

    }
}
