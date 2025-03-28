﻿using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace Auth.Models;

public class User : IdentityUser
{
    public string? Key { get; set; }
    public string RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; } = null!;
}