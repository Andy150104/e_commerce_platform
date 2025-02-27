﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwWishListDisplay
{
    public Guid WishlistId { get; set; }

    public string CustomerUsername { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
