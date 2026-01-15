using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Pages.DistributionCenters
{
    public class DeleteModel : PageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public DeleteModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DistributionCenter DistributionCenter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributioncenter = await _context.DistributionCenter.FirstOrDefaultAsync(m => m.Id == id);

            if (distributioncenter is not null)
            {
                DistributionCenter = distributioncenter;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distributioncenter = await _context.DistributionCenter.FindAsync(id);
            if (distributioncenter != null)
            {
                DistributionCenter = distributioncenter;
                _context.DistributionCenter.Remove(DistributionCenter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
