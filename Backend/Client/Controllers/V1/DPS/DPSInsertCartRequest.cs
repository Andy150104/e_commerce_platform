namespace Client.Controllers.V1.DPS;

public class DPSInsertCartRequest : AbstractApiRequest
{
    public string CodeProduct { get; set; }
    
    public int Quantity { get; set; }
}