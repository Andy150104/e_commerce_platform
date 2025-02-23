using Client.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Client.Controllers.V1.OnlinePaymentScreen
{
    public class OPSBuyingPlanRequest : AbstractApiRequest
    {
        [Required(ErrorMessage = "Plan Id is required")]
        public Guid PlanId { get; set; }
        public Guid? VoucherId { get; set; }   
    }
}
