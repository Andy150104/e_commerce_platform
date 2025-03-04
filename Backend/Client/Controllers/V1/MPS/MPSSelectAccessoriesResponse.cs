namespace Client.Controllers.V1.MPS;

public class MPSSelectAccessoriesResponse : AbstractApiResponse<List<MPSSelectAccessoriesEntity>>
{
    public override List<MPSSelectAccessoriesEntity> Response { get; set; }
}

public class MPSSelectAccessoriesEntity
{
    public string AccessoryId { get; set; } = null!;

    public string AccessoryName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }

    public int? AverageRating { get; set; }

    public int? TotalReviews { get; set; }

    public int? TotalSold { get; set; }

    public int? TotalOrders { get; set; }
    
    public List<MPSSelectImageAccessories> ImageAccessoriesList { get; set; }
}

public class MPSSelectImageAccessories
{
    public Guid ImageId { get; set; }

    public string AccessoryId { get; set; }

    public string ImageUrl { get; set; }
}