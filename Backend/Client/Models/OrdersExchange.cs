using System;
using System.Collections.Generic;

namespace server.Models;

public partial class OrdersExchange
{
    public Guid OrderExchangeId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ExchangeId { get; set; }

    public virtual Exchange? Exchange { get; set; }
}
