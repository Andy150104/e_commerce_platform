﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    public string Username { get; set; } = null!;

    public string? AddressLine { get; set; }

    public string? Ward { get; set; }

    public string? City { get; set; }

    public string? District { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Province { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User UsernameNavigation { get; set; } = null!;
}
