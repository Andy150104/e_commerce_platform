using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class OrderDetail
{
    public Guid OrderDetailId { get; set; }

    public Guid OrderId { get; set; }

    public string AccessoryId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Accessory Accessory { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
