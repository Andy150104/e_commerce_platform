namespace Client.Controllers.V1.DPS;

public class DPSSelectItemRequest : AbstractApiRequest
{
    public byte SearchBy { get; set; }
    
    public byte? SortBy { get; set; }
    
    public decimal? MinimumPrice { get; set; }
    
    public decimal? MaximumPrice { get; set; }
    
    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }
    
    public int CurrentPage { get; set; }
    
    public int PageSize { get; set; }
}