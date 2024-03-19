using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationMedical.Models;

namespace WebApplicationMedical.Pages.Patient
{
    public class CreateModel : PageModel
    {
        private readonly WebApplicationMedical.Models.MedicalDbContext _context;

        public CreateModel(WebApplicationMedical.Models.MedicalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Gender"] = new SelectList(_context.DGenders, "Id", "Gender");
            //ViewData["InsurancePolicyId"] = new SelectList(_context.DInsurancePolicies, "Id", "Id");
            //ViewData["MedcardId"] = new SelectList(_context.DMedcards, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public DPatient DPatient { get; set; } = default!;
        [BindProperty]
        public DMedcard DMedcard { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DPatients == null || DPatient == null || _context.DMedcards == null || DMedcard == null)
            {
                return Page();
            }
            try
            {
                _context.DPatients.Add(DPatient);
                await _context.SaveChangesAsync();
                _context.DMedcards.Add(DMedcard);
                DMedcard.PatientId = DPatient.Id;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
