using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.ThirdParty;

public class MomoOrderLogicReturnRequest : AbstractApiRequest
{ 
    [Required(ErrorMessage = "OrderId is required")]
    public Guid OrderId { get; set; }
}