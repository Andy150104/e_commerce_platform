using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.MPS;

public class MSPInsertImageAccessoryRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "CodeAccessory is required")]
    public string CodeAccessory { get; set; }
    
    [Required(ErrorMessage = "Image is required")]
    public List<IFormFile> Images { get; set; }
}