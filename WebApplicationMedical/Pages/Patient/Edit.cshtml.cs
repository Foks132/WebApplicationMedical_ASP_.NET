using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationMedical.Models;

namespace WebApplicationMedical.Pages.Patient
{
    public class EditModel : PageModel
    {
        private readonly WebApplicationMedical.Models.MedicalDbContext _context;

        public EditModel(WebApplicationMedical.Models.MedicalDbContext context)
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

            var dpatient =  await _context.DPatients.FirstOrDefaultAsync(m => m.Id == id);
            if (dpatient == null)
            {
                return NotFound();
            }
            DPatient = dpatient;
           ViewData["Gender"] = new SelectList(_context.DGenders, "Id", "Gender");
           ViewData["InsurancePolicyId"] = new SelectList(_context.DInsurancePolicies, "Id", "Id");
           ViewData["MedcardId"] = new SelectList(_context.DMedcards, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DPatient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DPatientExists(DPatient.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DPatientExists(int id)
        {
          return (_context.DPatients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
