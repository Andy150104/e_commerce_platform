using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwWishListDisplay
{
    public Guid WishlistId { get; set; }

    public string CustomerUsername { get; set; } = null!;

    public string AccessoryId { get; set; } = null!;

    public string AccessoryName { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
