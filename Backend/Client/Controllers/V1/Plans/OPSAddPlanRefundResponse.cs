using Client.Controllers;

namespace Client.Controllers.V1.OnlinePaymentScreen;

public class OPSAddPlanRefundResponse : AbstractApiResponse<string>
{
    public override string Response { get; set; }
}