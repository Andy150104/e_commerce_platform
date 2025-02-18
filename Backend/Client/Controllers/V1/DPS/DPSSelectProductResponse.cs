namespace Client.Controllers.V1.DPS;

public class DPSSelectProductResponse : AbstractApiResponse<DPSSelectProductEntity>
{
    public override DPSSelectProductEntity Response { get; set; }
}

public class DPSSelectProductEntity
{
    public string CodeProduct { get; set; }

    public string NameProduct { get; set; }
    
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string Price { get; set; }
    
    public string Discount { get; set; }
    
    public string SalePrice { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }
    
    public Guid? WishlistId { get; set; }

    public List<DpsSelectProductListImageUrl> ImageUrls { get; set; }

    public int? AverageRating { get; set; }

    public int? TotalReviews { get; set; }
}

public class DpsSelectProductListImageUrl
{
    public string ImageUrl { get; set; }
}