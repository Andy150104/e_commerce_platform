using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.RefundRequestOrders;

public class RROInsertRefundRequestOrderRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "OrderId is required")]
    public Guid OrderId { get; set; }

    [Required(ErrorMessage = "RefundReason is required")]
    public string RefundReason { get; set; }
    
    [Required(ErrorMessage = "RefundAmount is required")]
    public string ImageUrl { get; set; }
}