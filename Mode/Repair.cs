using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Repair
{
    public int Id { get; set; }

    public int? AvtoId { get; set; }

    public int? EmployeeId { get; set; }

    public int? NodeId { get; set; }

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public DateTime? TotalWorkTime { get; set; }

    public int? NumOfRepairedNodes { get; set; }

    public virtual Avto? Avto { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual NodeToRepair? Node { get; set; }
}
