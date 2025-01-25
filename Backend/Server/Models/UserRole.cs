using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class UserRole
{
    public string UserId { get; set; } = null!;

    public long RoleId { get; set; }
}
