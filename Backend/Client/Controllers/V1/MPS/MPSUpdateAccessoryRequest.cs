using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.V1.MPS;

public class MPSUpdateAccessoryRequest : AbstractApiRequest
{
    public string CodeAccessory { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? Discount { get; set; }

    public string? ShortDescription { get; set; }

    public Guid? CategoryId { get; set; }

    public List<IFormFile>? Images { get; set; }

    public List<Guid>? ImageId { get; set; }
}