namespace Client.Controllers.V1.DPS;

public class DPSDeleteCartItemRequest : AbstractApiRequest
{
    public string CodeProduct { get; set; }
}