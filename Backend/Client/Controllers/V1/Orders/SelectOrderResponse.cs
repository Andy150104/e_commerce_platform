namespace Client.Controllers.V1.Orders;

public class SelectOrderResponse : AbstractApiResponse<SelectOrderEntity>
{
    public override SelectOrderEntity Response { get; set; }
}

public class SelectOrderEntity
{
    public Guid OrderId { get; set; }
    
    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
    
    public byte Status { get; set; }
    
    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    
    public List<SelectOrderDetails> OrderDetails { get; set; }
}

public class SelectOrderDetails
{
    public Guid OrderDetailId { get; set; }
    
    public string AccessoryId { get; set; }
    
    public string AccessoryName { get; set; }

    public int Quantity { get; set; }

    public decimal OriginalPrice { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal DiscountPercent { get; set; }

    public decimal DiscountedPrice { get; set; }
}