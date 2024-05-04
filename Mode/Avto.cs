using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Avto
{
    public int Id { get; set; }

    public int? FacilityId { get; set; }

    public int? StatusId { get; set; }

    public int? MarkaId { get; set; }

    public int? TypeAvtoId { get; set; }

    public string? Color { get; set; }

    public int? Weight { get; set; }

    public int? Cost { get; set; }

    public int? LiftingCapacity { get; set; }

    public int? PeopleCapacity { get; set; }

    public int? Horsepower { get; set; }

    public DateTime? ReceptionDateTime { get; set; }

    public DateTime? DisposalDateTime { get; set; }

    public virtual ICollection<Drive> Drives { get; set; } = new List<Drive>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Facility? Facility { get; set; }

    public virtual MarkaAvto? Marka { get; set; }

    public virtual ICollection<Repair> Repairs { get; set; } = new List<Repair>();

    public virtual StatusAvto? Status { get; set; }

    public virtual TypeAvto? TypeAvto { get; set; }
}
