namespace Client.Controllers.V1.RefundRequestOrders;

public class RROSelectRefundRequestOrdersResponse : AbstractApiResponse<List<RROSelectRefundRequestOrdersEntity>>
{
    public override List<RROSelectRefundRequestOrdersEntity> Response { get; set; }
}

public class RROSelectRefundRequestOrdersEntity
{
    public Guid RefundId { get; set; }

    public Guid OrderId { get; set; }

    public string UserName { get; set; }

    public decimal RefundAmount { get; set; }
    
    public byte RefundStatus { get; set; }

    public byte PaymentMethod { get; set; }
    
    public DateTime CreatedAt { get; set; }
}