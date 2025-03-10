namespace Client.Controllers.V1.TOS;

public class SelectOrdersRequest : AbstractApiRequest
{
    public Guid? OrderId { get; set; }
}