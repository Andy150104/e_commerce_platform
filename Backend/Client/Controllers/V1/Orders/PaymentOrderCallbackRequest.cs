using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Orders;

public class PaymentOrderCallbackRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Order Id is required")]
    public Guid OrderId { get; set; }
    
    [Required(ErrorMessage = "Platform Id is required")]
    [Range(1, 2, ErrorMessage = "Platform Id must be 1 or 2")]
    public byte Platform { get; set; }
}