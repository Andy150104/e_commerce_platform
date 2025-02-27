using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Wishlist
{
    public Guid WishlistId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public virtual ICollection<BlindBox> BlindBoxes { get; set; } = new List<BlindBox>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User UserNameNavigation { get; set; } = null!;

    public virtual ICollection<WishlistItemBlindBox> WishlistItemBlindBoxes { get; set; } = new List<WishlistItemBlindBox>();

    public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
}
