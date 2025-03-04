using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.OPS;

public class OPSRefundPlanRequest : AbstractApiRequest
{
    [Required(ErrorMessage = "Refund Request Id is required")]
    public Guid RefundRequestId { get; set; }
}