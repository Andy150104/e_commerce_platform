using Client.Controllers;
using server.Models;

namespace server.Controllers.V1.HomeScreen
{
    public class HSShowPlanResponse : AbstractApiResponse<List<VwPlan>>
    {
        public override List<VwPlan> Response { get; set; }
        tạo entity
    }
}
