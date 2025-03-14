namespace Client.Controllers.V1.DPS;

public class DPSSelectCartItemResponse : AbstractApiResponse<DPSSelectCartItemEntity>
{
    public override DPSSelectCartItemEntity Response { get; set; }
}

public class DPSSelectCartItemEntity
{
    public decimal TotalPrice { get; set; }
    
    public List<DPSSelectCartItem> Items { get; set; }
}

public class DPSSelectCartItem
{
    public string AccessoryId { get; set; }

    public string AccessoryName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? UnitPrice { get; set; }
    
    public string ShortDescription { get; set; }
    
    public List<DPSSelectCartItemImages> Images { get; set; }
}

public class DPSSelectCartItemImages
{
    public string ImageUrl { get; set; }
}