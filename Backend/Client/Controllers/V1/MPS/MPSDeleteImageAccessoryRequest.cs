using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.MPS;

public class MPSDeleteImageAccessoryRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "ImageId is required")]
    public List<Guid> ImageId { get; set; }
}