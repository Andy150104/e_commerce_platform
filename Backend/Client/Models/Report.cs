using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Report
{
    public Guid ReportId { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public string Username { get; set; } = null!;

    public virtual User UsernameNavigation { get; set; } = null!;
}
