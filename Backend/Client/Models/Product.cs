using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

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

    public decimal? Discount { get; set; }

    public string? ShortDescription { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual Wishlist? Wishlist { get; set; }
}
