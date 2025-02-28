using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwProductDisplay
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ChildCategoryName { get; set; }

    public string? ParentCategoryName { get; set; }

    public int? AverageRating { get; set; }

    public int? TotalReviews { get; set; }

    public int? TotalSold { get; set; }

    public int? TotalOrders { get; set; }
}
