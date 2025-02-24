using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Queue
{
    public Guid QueueId { get; set; }

    public string Description { get; set; } = null!;

    public Guid? ExchangeId { get; set; }

    public string? Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Exchange? Exchange { get; set; }

    public virtual ICollection<OrdersExchange> OrdersExchanges { get; set; } = new List<OrdersExchange>();
}