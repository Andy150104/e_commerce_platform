namespace Client.Controllers.V1.DPS;

public class DPSUpdateCartItemRequest : AbstractApiRequest
{
    public int Quantity { get; set; }

    public string AccessoryId { get; set; }
}