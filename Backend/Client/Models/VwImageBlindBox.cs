using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwImageBlindBox
{
    public Guid ImageId { get; set; }

    public Guid BlindBoxId { get; set; }

    public string ImageUrl { get; set; } = null!;
}
