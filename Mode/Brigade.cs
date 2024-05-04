using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Brigade
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Brigadier> Brigadiers { get; set; } = new List<Brigadier>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
