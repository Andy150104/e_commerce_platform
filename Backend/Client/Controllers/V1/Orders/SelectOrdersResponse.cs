namespace Client.Controllers.V1.TOS;

public class SelectOrdersResponse : AbstractApiResponse<List<SelectOrdersEntity>>
{
    public override List<SelectOrdersEntity> Response { get; set; }
}

public class SelectOrdersEntity
{
    public Guid OrderId { get; set; }
    
    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
    
    public byte Status { get; set; }
    
    public List<TOSSelectOrderDetails> OrderDetails { get; set; }
}

public class TOSSelectOrderDetails
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
