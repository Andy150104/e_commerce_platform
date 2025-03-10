using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwOrder
{
    public Guid OrderId { get; set; }

    public string Username { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public byte OrderStatus { get; set; }
}
