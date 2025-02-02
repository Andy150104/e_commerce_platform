﻿using System;
using System.Collections.Generic;

namespace server.Models;

public partial class VwUserAuthentication
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }
}
