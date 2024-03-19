using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DHospitalizationPatient
{
    public int PatientId { get; set; }

    public DateTime Date { get; set; }

    public virtual DPatient Patient { get; set; } = null!;
}
