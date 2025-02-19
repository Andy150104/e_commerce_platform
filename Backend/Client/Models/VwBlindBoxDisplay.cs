using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwBlindBoxDisplay
{
    public Guid BlindBoxId { get; set; }

    public Guid? ExchangeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public Guid? WishlistId { get; set; }

    public string? ImageUrl { get; set; }
}
