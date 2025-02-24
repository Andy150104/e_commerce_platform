using Client.Controllers;
using server.Models;

namespace Client.Controllers.V1.OnlinePaymentScreen
{
    public class OPSRefundPlanResponse: AbstractApiResponse<string>
    {
        public override string Response { get; set; }
    }
}
