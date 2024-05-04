using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Route
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? StartPoint { get; set; }

    public string? EndPoint { get; set; }

    public decimal? Distance { get; set; }

    public virtual ICollection<Drive> Drives { get; set; } = new List<Drive>();
}
