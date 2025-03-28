﻿using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwEmailTemplateOrderScreen
{
    public int Id { get; set; }

    public DateTime CreateAt { get; set; }

    public string CreateBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime UpdateAt { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ScreenName { get; set; } = null!;
}
