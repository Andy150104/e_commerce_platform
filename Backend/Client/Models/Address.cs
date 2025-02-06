using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    public Guid? UserId { get; set; }

    public string? AddressLine { get; set; }

    public string? Ward { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? District { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? UpdateBy { get; set; }

    public virtual User? User { get; set; }
}
