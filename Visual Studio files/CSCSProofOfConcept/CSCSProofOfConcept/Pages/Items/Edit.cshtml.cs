using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Pages.Items
{
    public class EditModel : DCNamePageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public EditModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item =  await _context.Item
                .Include(m => m.DistributionCenter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            Item = item;

            PopulateDCDropDownList(_context, Item.DistributionCenterId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var itemToUpdate = await _context.Item.FindAsync(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Item>(
                itemToUpdate,
                "item",
                s => s.Id, s => s.Name, s => s.IsActive, s => s.Specifications, s => s.DistributionCenterId))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateDCDropDownList(_context, itemToUpdate.DistributionCenterId);
            return Page();

        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
