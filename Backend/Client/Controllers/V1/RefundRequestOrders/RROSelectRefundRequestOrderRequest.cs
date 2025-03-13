namespace Client.Controllers.V1.RefundRequestOrders;

public class RROSelectRefundRequestOrderRequest : AbstractApiRequest
{
    public Guid RefundId { get; set; }
}