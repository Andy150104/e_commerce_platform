using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Queue
{
    public Guid QueueId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ExchangeId { get; set; }

    public string? Status { get; set; }

    public virtual Exchange? Exchange { get; set; }

    public virtual Product? Product { get; set; }
}
