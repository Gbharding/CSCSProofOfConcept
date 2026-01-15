using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Pages.Items
{
    public class CreateModel : DCNamePageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public CreateModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDCDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyItem = new Item();

            if (await TryUpdateModelAsync<Item>(
                 emptyItem,
                 "item",
                 s => s.Id, s => s.Name, s => s.IsActive, s => s.Specifications, s => s.DistributionCenter, s => s.DistributionCenterId))
            {
                _context.Item.Add(emptyItem);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateDCDropDownList(_context, emptyItem.DistributionCenterId);
            return Page();
        }
    }
}
