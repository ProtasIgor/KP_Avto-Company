using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? License { get; set; }

    public string? Аddress { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<NachalnikTsekha> NachalnikTsekhas { get; set; } = new List<NachalnikTsekha>();
}
