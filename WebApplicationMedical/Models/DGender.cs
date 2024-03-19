using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMedical.Models;

public partial class DGender
{
    public int Id { get; set; }

    [DisplayName("Пол"), Required(ErrorMessage = "Укажите пол")]
    public string? Gender { get; set; }

    public virtual ICollection<DPatient> DPatients { get; set; } = new List<DPatient>();
}
