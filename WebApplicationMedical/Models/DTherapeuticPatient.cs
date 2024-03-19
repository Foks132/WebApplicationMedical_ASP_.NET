using System;
using System.Collections.Generic;

namespace WebApplicationMedical.Models;

public partial class DTherapeuticPatient
{
    public int? PatientId { get; set; }

    public int? TherapeuticServiceId { get; set; }

    public int? DoctorId { get; set; }

    public int? Type { get; set; }

    public string? Name { get; set; }

    public string? Result { get; set; }

    public string? Recommendation { get; set; }

    public int Id { get; set; }

    public virtual DPatient? Patient { get; set; }

    public virtual DTherapeuticService? TherapeuticService { get; set; }

    public virtual DTherapeuticType? TypeNavigation { get; set; }
}
