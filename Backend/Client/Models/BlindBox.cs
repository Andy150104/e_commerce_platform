using System;
using System.Collections.Generic;

namespace server.Models;

public partial class BlindBox
{
    public Guid BlindBoxId { get; set; }

    public Guid ExchangeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string Username { get; set; } = null!;

    public virtual Exchange Exchange { get; set; } = null!;

    public virtual ICollection<Queue> Queues { get; set; } = new List<Queue>();

    public virtual User UsernameNavigation { get; set; } = null!;

    public virtual ICollection<ImagesBlindBox> ImagesBlindBoxes { get; set; } = new List<ImagesBlindBox>();

}
