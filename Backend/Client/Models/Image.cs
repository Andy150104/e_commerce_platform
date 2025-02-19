using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class Image
{
    public Guid ImageId { get; set; }

    public string ProductId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
