using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Cart
{
    public Guid CartId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
