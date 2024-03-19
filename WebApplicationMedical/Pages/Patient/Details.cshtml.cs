using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationMedical.Models;

namespace WebApplicationMedical.Pages.Patient
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplicationMedical.Models.MedicalDbContext _context;

        public DetailsModel(WebApplicationMedical.Models.MedicalDbContext context)
        {
            _context = context;
        }

        public DPatient DPatient { get; set; } = default!;
        
        public string QrCode;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DPatients == null)
            {
                return NotFound();
            }

            QrCode = new QrCode().CrateQrCode(Url.PageLink("Details", "Id", new { id = id }));
            var dpatient = await _context.DPatients.FirstOrDefaultAsync(m => m.Id == id);
            var dgender = await _context.DGenders.FirstOrDefaultAsync(m => m.Id == dpatient.Gender);
            var dmedcard = await _context.DMedcards.FirstOrDefaultAsync(m => m.Id == dpatient.MedcardId);
            var dinsurance = await _context.DInsurancePolicies.FirstOrDefaultAsync(m => m.Id == dpatient.InsurancePolicyId);
            if (dpatient == null)
            {
                return NotFound();
            }
            else
            {
                DPatient = dpatient;
            }
            return Page();
        }
    }
}
