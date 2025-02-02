using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Exchange
{
    public Guid ExchangeId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? OrderExchangeId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<OrdersExchange> OrdersExchanges { get; set; } = new List<OrdersExchange>();

    public virtual Product? Product { get; set; }

    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();
}
