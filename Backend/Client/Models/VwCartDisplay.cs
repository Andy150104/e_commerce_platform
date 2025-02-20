﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwCartDisplay
{
    public Guid CartId { get; set; }

    public string CustomerUsername { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? ShortDescription { get; set; }

    public int Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? CartCreatedAt { get; set; }

    public DateTime? CartUpdatedAt { get; set; }
}
