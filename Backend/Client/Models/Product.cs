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

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? UpdateBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? WishlistId { get; set; }

    public Guid? UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Exchange> Exchanges { get; set; } = new List<Exchange>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual User? User { get; set; }

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
