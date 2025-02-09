using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Wishlist
{
    public Guid WishlistId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
