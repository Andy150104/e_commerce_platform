using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? WishlistId { get; set; }

    public string Username { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual Wishlist? Wishlist { get; set; }
}
