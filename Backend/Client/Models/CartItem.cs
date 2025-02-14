﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class CartItem
{
    public Guid CartItemId { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;
}
