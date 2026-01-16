using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Pages.Projects
{
    public class CreateModel : ItemNamePageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public CreateModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateItemDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyProject = new Project();

            if (await TryUpdateModelAsync<Project>(
                emptyProject,
                "project",
                p => p.Id, p => p.ItemId, p => p.Name, p => p.PrjType, p => p.OldSpec, p => p.NewSpec, p => p.PrjPrice, p => p.FreightStrat))
            {
                if (String.IsNullOrWhiteSpace(emptyProject.Item?.Name))
                {
                    emptyProject.Item = null;
                    emptyProject.ItemId = null;
                }
                emptyProject.PrjStage = 1;
                _context.Project.Add(emptyProject);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateItemDropDownList(_context, emptyProject.ItemId);
            return Page();
        }
    }
}
