namespace Client.Controllers.V1.TOS;

public class InsertOrderResponse : AbstractApiResponse<InsertOrderResponseEntity>
{
    public override InsertOrderResponseEntity Response { get; set; }
}

public class InsertOrderResponseEntity
{
    public string PaymentUrl { get; set; }
    
    public string GHNCode { get; set; }
}
