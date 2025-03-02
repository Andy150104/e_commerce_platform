using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwImageAccessory
{
    public Guid ImageId { get; set; }

    public string AccessoryId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
}
