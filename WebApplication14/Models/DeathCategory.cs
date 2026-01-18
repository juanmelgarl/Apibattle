using System;
using System.Collections.Generic;

namespace WebApplication14.Models;

public partial class DeathCategory
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public virtual ICollection<Death> Deaths { get; set; } = new List<Death>();
}
