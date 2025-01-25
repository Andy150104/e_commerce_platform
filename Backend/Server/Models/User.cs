using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class User
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? LockDate { get; set; }

    public byte AuthFailedCount { get; set; }

    public bool IsEnabled { get; set; }

    public string Email { get; set; } = null!;
    
    public long RoleId { get; set; }

    public virtual Role Role { get; set; }
}
