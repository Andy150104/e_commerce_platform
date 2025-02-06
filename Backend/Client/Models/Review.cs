using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Review
{
    public Guid ReviewId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ProductId { get; set; }

    public string? ReviewContent { get; set; }

    public bool? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? Description { get; set; }

    public string? ReplyReviewContent { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
