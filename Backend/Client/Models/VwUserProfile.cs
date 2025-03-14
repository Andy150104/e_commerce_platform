﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwUserProfile
{
    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ImageUrl { get; set; }

    public DateOnly? BirthDate { get; set; }

    public byte? Gender { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
