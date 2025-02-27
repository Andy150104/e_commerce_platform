using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Voucher
{
    public Guid VoucherId { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public Guid OrderId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderPlan> OrderPlans { get; set; } = new List<OrderPlan>();
}
