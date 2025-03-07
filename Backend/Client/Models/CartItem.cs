﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class CartItem
{
    public Guid CartItemId { get; set; }

    public Guid CartId { get; set; }

    public int Quantity { get; set; }

    public string AccessoryId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Accessory Accessory { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;
}
