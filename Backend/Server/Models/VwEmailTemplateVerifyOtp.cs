using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class VwEmailTemplateVerifyOtp
{
    public int Id { get; set; }

    public string EmailBody { get; set; } = null!;

    public string EmailTitle { get; set; } = null!;

    public string ScreenName { get; set; } = null!;
}
