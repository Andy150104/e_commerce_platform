using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.MPS;

public class MPSInsertProductRequest : AbstractApiRequest
{
    public string? Description { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    public decimal Price { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
    
    [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
    public decimal? Discount { get; set; }
    
    public string? ShortDescription { get; set; }
    
    public Guid? CategoryId { get; set; }

    public List<IFormFile> Images { get; set; }
}