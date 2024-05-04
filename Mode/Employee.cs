using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Employee
{
    public int Id { get; set; }

    public int? BrigadeId { get; set; }

    public int? TypeEmployeeId { get; set; }

    public int? AvtoId { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public decimal? Experience { get; set; }

    public virtual Avto? Avto { get; set; }

    public virtual Brigade? Brigade { get; set; }

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();

    public virtual TypeEmployee? TypeEmployee { get; set; }
}
