using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class CartItem
{
    public Guid CartItemId { get; set; }

    public Guid CartId { get; set; }

    public string ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
