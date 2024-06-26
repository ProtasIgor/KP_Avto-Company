﻿using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class TypeEmployee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
