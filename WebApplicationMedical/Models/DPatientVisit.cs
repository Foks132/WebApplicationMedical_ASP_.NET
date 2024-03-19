using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DPatientVisit
{
    public int PatientId { get; set; }

    public DateTime Date { get; set; }

    public int? AppointmentId { get; set; }

    public virtual DPatient Patient { get; set; } = null!;
}
