using System;
using System.Collections.Generic;

namespace server.Models;

public partial class OrdersExchange
{
    public Guid OrderExchangeId { get; set; }

    public Guid ExchangeId { get; set; }

    public Guid QueueId { get; set; }

    public virtual Exchange Exchange { get; set; } = null!;

    public virtual Queue Queue { get; set; } = null!;
}
