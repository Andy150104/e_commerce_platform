using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Review
{
    public Guid ReviewId { get; set; }

    public string Username { get; set; } = null!;

    public Guid AccessoryId { get; set; }

    public string? ReviewContent { get; set; }

    public int? Rating { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual User UsernameNavigation { get; set; } = null!;
}
