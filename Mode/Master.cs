using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Master
{
    public int Id { get; set; }

    public int? NachalnikUchastkaId { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Brigadier> Brigadiers { get; set; } = new List<Brigadier>();

    public virtual NachalnikUchastka? NachalnikUchastka { get; set; }
}
