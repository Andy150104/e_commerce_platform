using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? UserId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
