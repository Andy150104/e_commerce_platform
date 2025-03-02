using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.DPS;

public class DPSInsertCartRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "CodeAccessory is required")]
    public string CodeAccessory { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}