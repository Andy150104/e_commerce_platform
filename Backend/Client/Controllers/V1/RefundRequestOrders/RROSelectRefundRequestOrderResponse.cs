namespace Client.Controllers.V1.RefundRequestOrders;

public class RROSelectRefundRequestOrderResponse : AbstractApiResponse<RROSelectRefundRequestOrderEntity>
{
    public override RROSelectRefundRequestOrderEntity Response { get; set; }
}

public class RROSelectRefundRequestOrderEntity
{
    public Guid RefundId { get; set; }

    public Guid OrderId { get; set; }

    public string UserName { get; set; } = null!;

    public decimal RefundAmount { get; set; }

    public string RefundReason { get; set; } = null!;

    public byte RefundStatus { get; set; }

    public byte PaymentMethod { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public string? ProcessedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string? RejectedReason { get; set; }
}