using Client.Controllers.V1.TOS;

namespace Client.Controllers.V1.Orders;

public class PaymentOrderCallbackResponse : AbstractApiResponse<MomoResponse>
{
    public override MomoResponse Response { get; set; }
}