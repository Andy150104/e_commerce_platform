using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwRefundRequestOrder
{
    public Guid RefundId { get; set; }

    public Guid OrderId { get; set; }

    public string UserName { get; set; } = null!;

    public decimal RefundAmount { get; set; }

    public string RefundReason { get; set; } = null!;

    public byte RefundStatus { get; set; }

    public byte PaymentMethod { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public string? ProcessedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string? RejectedReason { get; set; }
}
