using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DPatientDiagnosis
{
    public int PatientId { get; set; }

    public int DiagnosisId { get; set; }

    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public virtual DDiagnosis Diagnosis { get; set; } = null!;

    public virtual DPatient Patient { get; set; } = null!;
}
