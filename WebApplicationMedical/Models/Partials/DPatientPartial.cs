using System.ComponentModel;

namespace WebApplicationMedical.Models;

partial class DPatient
{
    [DisplayName("ФИО")]
    public string FIO
    {
        get
        {
            return $"{this.LastName} {this.FirstName} {this.SecondaryName}";
        }
    }
}
