using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class BlindBox
{
    public Guid BlindBoxId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Username { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? WishlistId { get; set; }

    public virtual ICollection<Exchange> Exchanges { get; set; } = new List<Exchange>();

    public virtual ICollection<ImagesBlindBox> ImagesBlindBoxes { get; set; } = new List<ImagesBlindBox>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual Wishlist? Wishlist { get; set; }

    public virtual ICollection<WishlistItemBlindBox> WishlistItemBlindBoxes { get; set; } = new List<WishlistItemBlindBox>();
}
