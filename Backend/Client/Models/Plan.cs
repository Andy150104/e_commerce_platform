using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Plan
{
    public Guid PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationMonths { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<OrderPlan> OrderPlans { get; set; } = new List<OrderPlan>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}