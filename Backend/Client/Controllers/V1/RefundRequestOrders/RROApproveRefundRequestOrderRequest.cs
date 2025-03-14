using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.RefundRequestOrders;

public class RROApproveRefundRequestOrderRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "RefundRequestId is required")]
    public Guid RefundId { get; set; }
    
    [Required(ErrorMessage = "IsApproved is required")]
    public bool IsApproved { get; set; }
    
    public string? RejectedReason { get; set; }
}