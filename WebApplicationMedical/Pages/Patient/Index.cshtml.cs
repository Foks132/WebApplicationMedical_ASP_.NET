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
    public class IndexModel : PageModel
    {
        private readonly WebApplicationMedical.Models.MedicalDbContext _context;

        public IndexModel(WebApplicationMedical.Models.MedicalDbContext context)
        {
            _context = context;
        }

        public IList<DPatient> DPatient { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DPatients != null)
            {
                DPatient = await _context.DPatients
                .Include(d => d.GenderNavigation)
                .Include(d => d.InsurancePolicy)
                .Include(d => d.Medcard).ToListAsync();
            }
        }
    }
}
