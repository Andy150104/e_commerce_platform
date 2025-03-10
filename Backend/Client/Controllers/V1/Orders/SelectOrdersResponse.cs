namespace Client.Controllers.V1.TOS;

public class SelectOrdersResponse : AbstractApiResponse<List<SelectOrdersEntity>>
{
    public override List<SelectOrdersEntity> Response { get; set; }
}

public class SelectOrdersEntity
{
    public Guid OrderId { get; set; }

    public string Username { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public byte Status { get; set; }
    
    public List<TOSSelectOrderDetails> OrderDetails { get; set; }
}

public class TOSSelectOrderDetails
{
    public Guid OrderDetailId { get; set; }
    
    public string AccessoryId { get; set; }
    
    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal OriginalPrice { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public string? ProductDescription { get; set; }
    
    public decimal? DiscountPercent { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }
}
