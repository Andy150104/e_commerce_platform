using Client.Controllers.V1.MomoPayment.MomoServices;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.V1.MomoServices
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private IMomoService _momoSerivce;
        public MomoController(IMomoService momoSerivce)
        {
            _momoSerivce = momoSerivce;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentMomo(MomoExecuteResponseModel model)
        {
            var response = await _momoSerivce.CreatePaymentAsync(model);
            return Ok(response);    
        }
        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoSerivce.PaymentExecuteAsync(HttpContext.Request.Query); 

            return Ok(response);
        }

    }
}
