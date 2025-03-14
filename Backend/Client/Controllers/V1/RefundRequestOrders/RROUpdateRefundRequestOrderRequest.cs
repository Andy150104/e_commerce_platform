using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.RefundRequestOrders;

public class RROUpdateRefundRequestOrderRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "RefundId is required")]
    public Guid RefundId { get; set; }
    public string? RefundReason { get; set; }
    
    public string? ImageUrl { get; set; }
}