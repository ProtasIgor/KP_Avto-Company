using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class NodeToRepair
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Cost { get; set; }

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();
}
