using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class OrderDetail
{
    public Guid OrderDetailId { get; set; }

    public Guid OrderId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
