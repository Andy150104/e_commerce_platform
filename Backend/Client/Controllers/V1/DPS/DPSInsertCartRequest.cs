using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.DPS;

public class DPSInsertCartRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "CodeProduct is required")]
    public string CodeProduct { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}