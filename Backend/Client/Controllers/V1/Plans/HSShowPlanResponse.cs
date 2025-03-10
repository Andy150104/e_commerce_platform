using Client.Models;

namespace Client.Controllers.V1.HS;

public class HSShowPlanResponse : AbstractApiResponse<List<HSShowPlanEntity>>
{
    public override List<HSShowPlanEntity> Response { get; set; }
}

public class HSShowPlanEntity
{
    public Guid PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationMonths { get; set; }
}