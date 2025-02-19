using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class ImagesBlindBox
{
    public Guid ImageId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public Guid BlindBoxId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual BlindBox BlindBox { get; set; } = null!;
}
