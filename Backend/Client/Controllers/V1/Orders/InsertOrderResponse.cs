namespace Client.Controllers.V1.TOS;

public class InsertOrderResponse : AbstractApiResponse<InsertOrderResponseEntity>
{
    public override InsertOrderResponseEntity Response { get; set; }
}

public class InsertOrderResponseEntity
{
    public MomoResponse Momo { get; set; }
    public string GHNCode { get; set; }
}

public class MomoResponse
{
    public string PaymentUrl { get; set; }
    public string QrCodeUrl { get; set; }
    public string Deeplink { get; set; }
    public string DeeplinkWebInApp { get; set; }
}
