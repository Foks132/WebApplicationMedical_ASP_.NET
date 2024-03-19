using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace WebApplicationMedical.Models
{
    public class SearchPatient
    {
        [DisplayName("СНИЛС")]
        public string? MedcardId { get; set; }
    }
}
