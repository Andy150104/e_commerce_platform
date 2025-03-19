using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.V1.MPS;

public class MPSUpdateAccessoryRequest : AbstractApiRequest
{
    public string CodeAccessory { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Price must be a whole number (integer)")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value")]
    public int Quantity { get; set; }

    [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
    public decimal? Discount { get; set; }

    public string? ShortDescription { get; set; }

    public Guid? CategoryId { get; set; }

    public List<IFormFile>? Images { get; set; }

    public List<Guid>? ImageId { get; set; }
}