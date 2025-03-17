using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class OrdersExchange
{
    public Guid OrderExchangeId { get; set; }

    public Guid ExchangeId { get; set; }

    public Guid QueueId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual Exchange Exchange { get; set; } = null!;

    public virtual Queue Queue { get; set; } = null!;
}
