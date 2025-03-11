using System;
using System.Collections.Generic;

namespace Client.Models;

public partial class VwCategoriesDisplay
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public Guid? ParentId { get; set; }
}
