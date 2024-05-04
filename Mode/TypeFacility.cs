using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class TypeFacility
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
}
