namespace Client.Controllers.V1.DPS;

public class DPSSelectAccessoryResponse : AbstractApiResponse<DPSSelectAccessoryEntity>
{
    public override DPSSelectAccessoryEntity Response { get; set; }
}

public class DPSSelectAccessoryEntity
{
    public string CodeAccessory { get; set; }

    public string NameAccessory { get; set; }
    
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string Price { get; set; }
    
    public string Discount { get; set; }
    
    public string? SalePrice { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }
    
    public Guid? WishlistId { get; set; }

    public List<DpsSelectAccessoryListImageUrl> ImageUrls { get; set; }

    public int? AverageRating { get; set; }

    public int? TotalReviews { get; set; }
}

public class DpsSelectAccessoryListImageUrl
{
    public string ImageUrl { get; set; }
}