using Client.Models;

namespace Client.Controllers.V1.HomeScreen
{
    public class HSShowPlanResponse : AbstractApiResponse<List<VwPlan>>
    {
        public override List<VwPlan> Response { get; set; }
    }
}
