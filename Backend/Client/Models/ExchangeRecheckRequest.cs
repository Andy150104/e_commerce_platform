using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class ExchangeRecheckRequest
{
    public Guid RequestId { get; set; }

    public Guid ExchangeId { get; set; }

    public string? Description { get; set; }

    public byte Status { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Exchange Exchange { get; set; } = null!;
}
