namespace Client.Controllers.V1.RefundRequestOrders;

public class RRODeleteRefundRequestOrderRequest : AbstractApiRequest
{
    public Guid RefundId { get; set; }
}