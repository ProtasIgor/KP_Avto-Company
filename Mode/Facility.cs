using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Facility
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public int? TypeFacilityId { get; set; }

    public string? Name { get; set; }

    public int? Square { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Avto> Avtos { get; set; } = new List<Avto>();

    public virtual Company? Company { get; set; }

    public virtual TypeFacility? TypeFacility { get; set; }
}
