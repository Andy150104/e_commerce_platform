namespace Client.Controllers.V1.DPS;

public class DPSSelectCartItemResponse : AbstractApiResponse<List<DPSSelectCartItemEntity>>
{
    public override List<DPSSelectCartItemEntity> Response { get; set; }
}

public class DPSSelectCartItemEntity
{
    public string AccessoryId { get; set; }

    public string AccessoryName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? TotalPrice { get; set; }
    
    public string ShortDescription { get; set; }
    
    public List<DPSSelectCartItemImages> Images { get; set; }
}

public class DPSSelectCartItemImages
{
    public string ImageUrl { get; set; }
}