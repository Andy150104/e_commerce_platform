namespace Client.Controllers.V1.DPS;

public class DPSUpdateCartItemRequest : AbstractApiRequest
{
    public int Quantity { get; set; }

    public string ProductId { get; set; }
}