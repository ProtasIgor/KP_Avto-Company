using System;
using System.Collections.Generic;

namespace AvtoSityApp.Mode;

public partial class Drive
{
    public int Id { get; set; }

    public int? AvtoId { get; set; }

    public int? TypeDriveId { get; set; }

    public int? RouteId { get; set; }

    public decimal? Distance { get; set; }

    public decimal? FuelConsumption { get; set; }

    public int? Сapacity { get; set; }

    public int? CargoWeight { get; set; }

    public DateTime? StartDrive { get; set; }

    public DateTime? FinishDrive { get; set; }

    public virtual Avto? Avto { get; set; }

    public virtual Route? Route { get; set; }

    public virtual TypeDrive? TypeDrive { get; set; }
}
