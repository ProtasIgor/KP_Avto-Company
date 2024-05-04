using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Brigadier
{
    public int Id { get; set; }

    public int? BrigadeId { get; set; }

    public int? MasterId { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public virtual Brigade? Brigade { get; set; }

    public virtual Master? Master { get; set; }
}
