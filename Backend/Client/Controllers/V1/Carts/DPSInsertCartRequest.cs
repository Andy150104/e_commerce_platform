using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.DPS;

public class DPSInsertCartRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "CodeAccessory is required")]
    public string CodeAccessory { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value")]
    public int Quantity { get; set; }
}