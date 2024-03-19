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
    public class DeleteModel : PageModel
    {
        private readonly WebApplicationMedical.Models.MedicalDbContext _context;

        public DeleteModel(WebApplicationMedical.Models.MedicalDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DPatient DPatient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DPatients == null)
            {
                return NotFound();
            }

            var dpatient = await _context.DPatients.FirstOrDefaultAsync(m => m.Id == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DPatients == null)
            {
                return NotFound();
            }
            var dpatient = await _context.DPatients.FindAsync(id);

            if (dpatient != null)
            {
                DPatient = dpatient;
                _context.DPatients.Remove(DPatient);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
