
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMedical.Models;

public partial class DPatient
{
    public int Id { get; set; }

    [DisplayName("Имя"), Required(ErrorMessage = "Введите имя")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Фамилия"), Required(ErrorMessage = "Введите фамилию")]
    public string LastName { get; set; } = null!;

    [DisplayName("Отчество"), Required(ErrorMessage = "Введите отчество")]
    public string SecondaryName { get; set; } = null!;

    [DisplayName("Паспорт"), Required(ErrorMessage = "Введите серию номер паспорта")]
    public string? Passport { get; set; }

    [DisplayName("Телефон"), Required(ErrorMessage = "Введите номер телефона")]
    public string? Phone { get; set; }

    [DisplayName("Почта"), Required(ErrorMessage = "Введите почту")]
    public string? Email { get; set; }

    [DisplayName("Пол"), Required(ErrorMessage = "Укажите пол")]
    public int Gender { get; set; }

    public string? InsurancePolicyId { get; set; }

    [DisplayName("Фото")]
    public string? Photo { get; set; }


    public string? MedcardId { get; set; }

    public virtual ICollection<DHospitalizationPatient> DHospitalizationPatients { get; set; } = new List<DHospitalizationPatient>();

    public virtual ICollection<DPatientDiagnosis> DPatientDiagnoses { get; set; } = new List<DPatientDiagnosis>();

    public virtual ICollection<DPatientDisease> DPatientDiseases { get; set; } = new List<DPatientDisease>();

    public virtual ICollection<DPatientVisit> DPatientVisits { get; set; } = new List<DPatientVisit>();

    public virtual ICollection<DTherapeuticPatient> DTherapeuticPatients { get; set; } = new List<DTherapeuticPatient>();


    [DisplayName("Пол")]
    public virtual DGender GenderNavigation { get; set; } = null!;

    [DisplayName("ОМС")]
    public virtual DInsurancePolicy? InsurancePolicy { get; set; }

    [DisplayName("СНИЛС")]
    public virtual DMedcard? Medcard { get; set; }
}
