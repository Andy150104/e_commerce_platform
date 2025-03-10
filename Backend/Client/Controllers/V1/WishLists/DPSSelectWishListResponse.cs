namespace Client.Controllers.V1.DPS;

public class DPSSelectWishListResponse : AbstractApiResponse<List<DPSSelectWishListEntity>>
{
    public override List<DPSSelectWishListEntity> Response { get; set; }
}

public class DPSSelectWishListEntity
{
    public string AccessoryId { get; set; } = null!;

    public string AccessoryName { get; set; } = null!;

    public string? ShortDescription { get; set; }
    
    public List<DPSSelectWishListImages> Images { get; set; }
}

public class DPSSelectWishListImages
{
    public string ImageUrl { get; set; }
}