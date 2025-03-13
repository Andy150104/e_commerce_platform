namespace Client.Controllers.V1.DPS;

public class DPSSelectItemResponse : AbstractApiResponse<DPSSelectItemEntity>
{
    public override DPSSelectItemEntity Response { get; set; }
}

public class DPSSelectItemEntity
{
    public int TotalRecords { get; set; } 
    
    public int TotalPages { get; set; }
    
    public List<ItemEntity> Items { get; set; }
}

public class ItemEntity
{
    public string CodeProduct { get; set; }

    public string NameAccessory { get; set; }
    
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string Price { get; set; }
    
    public string Discount { get; set; }
    
    public string SalePrice { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }
    
    public List<DpsSelectItemListImageUrl> ImageUrl { get; set; }

    public int? AverageRating { get; set; }

    public int? TotalReviews { get; set; }
    
    public Guid? ExchangeId { get; set; }

    public string FirstNameCreator { get; set; }
    
    public string LastNameCreator { get; set; }
}

public class DpsSelectItemListImageUrl
{
    public string ImageUrl { get; set; }
}