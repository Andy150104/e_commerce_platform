using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwProductDisplay
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }

    public Guid? WishlistId { get; set; }

    public string? ImageUrl { get; set; }

    public int? AverageRating { get; set; }

    public int? TotalReviews { get; set; }
}
