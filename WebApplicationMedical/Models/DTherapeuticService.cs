using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DTherapeuticService
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<DTherapeuticPatient> DTherapeuticPatients { get; set; } = new List<DTherapeuticPatient>();
}
