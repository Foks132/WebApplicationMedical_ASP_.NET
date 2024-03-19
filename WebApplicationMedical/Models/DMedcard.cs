using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMedical.Models;

public partial class DMedcard
{
    [DisplayName("СНИЛС"), Required(ErrorMessage = "Введите номер СНИЛС")]
    public string Id { get; set; } = null!;

    [DisplayName("Дата действия СНИЛС"), Required(ErrorMessage = "Укажите дату действия СНИЛС")]
    public string Date { get; set; } = null!;

    public int? PatientId { get; set; }

    public virtual ICollection<DPatient> DPatients { get; set; } = new List<DPatient>();
}
