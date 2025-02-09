using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Exchange
{
    public Guid ExchangeId { get; set; }

    public string? Status { get; set; }

    public virtual BlindBox? BlindBox { get; set; }

    public virtual ICollection<OrdersExchange> OrdersExchanges { get; set; } = new List<OrdersExchange>();

    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();
}
