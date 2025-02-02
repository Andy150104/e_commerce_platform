using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Report
{
    public Guid ReportId { get; set; }

    public Guid? UserId { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? UpdateBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? User { get; set; }
}
