using System;
using System.Collections.Generic;

namespace WebApplication14.Models;

public partial class Season
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}
