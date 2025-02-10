using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Queue
{
    public Guid QueueId { get; set; }

    public Guid BlindBoxId { get; set; }

    public Guid? ExchangeId { get; set; }

    public string? Status { get; set; }

    public virtual BlindBox BlindBox { get; set; } = null!;

    public virtual Exchange? Exchange { get; set; }

    public virtual ICollection<OrdersExchange> OrdersExchanges { get; set; } = new List<OrdersExchange>();
}
