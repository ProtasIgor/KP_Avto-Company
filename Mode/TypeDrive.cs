using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class TypeDrive
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Drive> Drives { get; set; } = new List<Drive>();
}
