using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class WishlistItemBlindBox
{
    public Guid WishlistItemId { get; set; }

    public Guid WishlistId { get; set; }

    public Guid BlindBoxId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public bool IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual BlindBox BlindBox { get; set; } = null!;

    public virtual Wishlist Wishlist { get; set; } = null!;
}
