using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class RefundPlanRequest
{
    public Guid RefundRequests { get; set; }

    public Guid OrderPlanId { get; set; }

    public string? Reason { get; set; }

    public byte Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int? ResultCode { get; set; }

    public string? ResultResponse { get; set; }

    public virtual OrderPlan OrderPlan { get; set; } = null!;
}
