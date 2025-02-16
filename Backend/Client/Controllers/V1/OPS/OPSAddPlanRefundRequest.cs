using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.OnlinePaymentScreen
{
    public class OPSAddPlanRefundRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "Order Plan Id is required")]
        public Guid OrderPlanId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Reason { get; set; }
    }
}
