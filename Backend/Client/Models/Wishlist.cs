using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Wishlist
{
    public Guid WishlistId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BlindBox> BlindBoxes { get; set; } = new List<BlindBox>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
