namespace Client.Controllers.V1.DPS;

public class DPSSelectWishListResponse : AbstractApiResponse<List<DPSSelectWishListEntity>>
{
    public override List<DPSSelectWishListEntity> Response { get; set; }
}

public class DPSSelectWishListEntity
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? ShortDescription { get; set; }
    
    public List<DPSSelectWishListImages> Images { get; set; }
}

public class DPSSelectWishListImages
{
    public string ImageUrl { get; set; }
}