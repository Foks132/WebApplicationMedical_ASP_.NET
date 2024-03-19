using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMedical.Models;

public partial class DInsurancePolicy
{
    [DisplayName("ОМС"), Required(ErrorMessage = "Введите номер ОМС")]
    public string Id { get; set; } = null!;

    [DisplayName("Дата действия ОМС"), Required(ErrorMessage = "Укажите дату действия ОМС")]
    public DateTime Date { get; set; }

    public int PatientId { get; set; }

    public virtual ICollection<DPatient> DPatients { get; set; } = new List<DPatient>();
}
