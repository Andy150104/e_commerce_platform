using System;
using System.Collections.Generic;

namespace server.Models;

public partial class VwPlan
{
    public Guid PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationMonths { get; set; }
}
