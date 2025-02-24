using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class OrderPlan
{
    public Guid OrderId { get; set; }

    public string Username { get; set; } = null!;

    public Guid PlanId { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public decimal Price { get; set; }

    public Guid? VoucherId { get; set; }

    public virtual Plan Plan { get; set; } = null!;

    public virtual ICollection<RefundPlanRequest> RefundPlanRequests { get; set; } = new List<RefundPlanRequest>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual Voucher? Voucher { get; set; }
}
