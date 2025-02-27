using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwImageProduct
{
    public Guid ImageId { get; set; }

    public string ProductId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
}
