using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class NachalnikTsekha
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<NachalnikUchastka> NachalnikUchastkas { get; set; } = new List<NachalnikUchastka>();
}
