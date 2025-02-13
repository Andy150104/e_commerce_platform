using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Exchange
{
    public Guid ExchangeId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual BlindBox? BlindBox { get; set; }

    public virtual ICollection<OrdersExchange> OrdersExchanges { get; set; } = new List<OrdersExchange>();

    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();
}
