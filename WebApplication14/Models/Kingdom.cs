using System;
using System.Collections.Generic;

namespace WebApplication14.Models;

public partial class Kingdom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
