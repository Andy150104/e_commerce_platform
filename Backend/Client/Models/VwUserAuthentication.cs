using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwUserAuthentication
{
    public string UserName { get; set; } = null!;

    public string? Email { get; set; }
}
