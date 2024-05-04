using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class MarkaAvto
{
    public int Id { get; set; }

    public string? Marka { get; set; }

    public string? Model { get; set; }

    public virtual ICollection<Avto> Avtos { get; set; } = new List<Avto>();
}
