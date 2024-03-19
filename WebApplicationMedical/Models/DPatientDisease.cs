using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DPatientDisease
{
    public int? PatientId { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public int Id { get; set; }

    public virtual DPatient? Patient { get; set; }
}
