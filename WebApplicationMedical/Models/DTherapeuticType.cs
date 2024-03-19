using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DTherapeuticType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DTherapeuticPatient> DTherapeuticPatients { get; set; } = new List<DTherapeuticPatient>();
}
