using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class NachalnikUchastka
{
    public int Id { get; set; }

    public int? NachalnikTsekhaId { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();

    public virtual NachalnikTsekha? NachalnikTsekha { get; set; }
}
