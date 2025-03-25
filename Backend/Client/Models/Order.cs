using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public string Username { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public byte Status { get; set; }

    public bool IsActive { get; set; }

    public Guid? AddressId { get; set; }

    public string? GhnCode { get; set; }

    public string? MomoUrl { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<RefundRequestsOrder> RefundRequestsOrders { get; set; } = new List<RefundRequestsOrder>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
