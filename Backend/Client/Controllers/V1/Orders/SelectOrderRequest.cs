using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.Orders;

public class SelectOrderRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "OrderId is required")]
    public Guid OrderId { get; set; }
}