using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DDiagnosis
{
    public int Id { get; set; }

    public string? Diagnosis { get; set; }

    public virtual ICollection<DPatientDiagnosis> DPatientDiagnoses { get; set; } = new List<DPatientDiagnosis>();
}
