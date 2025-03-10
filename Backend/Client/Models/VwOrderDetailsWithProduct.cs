using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwOrderDetailsWithProduct
{
    public Guid OrderDetailId { get; set; }

    public Guid OrderId { get; set; }

    public string Username { get; set; } = null!;

    public string AccessoryId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal? DiscountPercent { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
