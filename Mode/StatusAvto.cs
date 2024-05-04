using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class StatusAvto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Avto> Avtos { get; set; } = new List<Avto>();
}
