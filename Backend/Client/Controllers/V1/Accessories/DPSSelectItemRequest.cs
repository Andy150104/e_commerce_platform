using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Accessories;

public class DPSSelectItemRequest : AbstractApiRequest
{
    public byte SearchBy { get; set; }
    
    public byte? SortBy { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be a non-negative value")]
    public decimal? MinimumPrice { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Maximum price must be a non-negative value")]
    public decimal? MaximumPrice { get; set; }
    
    public string? NameAccessory { get; set; }
    
    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }
    
    public int CurrentPage { get; set; }
    
    public int PageSize { get; set; }
}