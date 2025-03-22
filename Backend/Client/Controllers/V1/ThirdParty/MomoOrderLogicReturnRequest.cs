using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.ThirdParty;

public class MomoOrderLogicReturnRequest : AbstractApiRequest
{ 
    [Required(ErrorMessage = "OrderId is required")]
    public Guid OrderId { get; set; }
    
    [Required(ErrorMessage = "GhnOrderCode is required")]
    public string GhnOrderCode { get; set; }
}