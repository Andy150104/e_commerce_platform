using Client.Controllers;
using server.Models;

namespace server.Controllers.V1.HomeScreen
{
    public class HSShowPlanResponse : AbstractApiResponse<List<HSShowPlanResponseEntity>>
    {
        public override List<HSShowPlanResponseEntity> Response { get; set; }
    }

    public class HSShowPlanResponseEntity
    {
        public Guid PlanId { get; set; }

        public string PlanName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int DurationMonths { get; set; }
    }

}
