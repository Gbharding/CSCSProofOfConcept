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

namespace CSCSProofOfConcept.Pages.Projects
{
    public class AdvanceModel : ItemNamePageModel
    {

        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public AdvanceModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; } = default!;

        public static int PrjStage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(m => m.Item)
                .Include(m => m.DistributionCenter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            Project = project;
            PrjStage = project.PrjStage;

            PopulateDCDropDownList(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Project).State = EntityState.Modified;
            _context.Entry(Project).Property(m => m.DueDate).IsModified = false;
            _context.Entry(Project).Property(m => m.Id).IsModified = false;
            _context.Entry(Project).Property(m => m.Name).IsModified = false;
            _context.Entry(Project).Property(m => m.Description).IsModified = false;
            _context.Entry(Project).Property(m => m.ItemId).IsModified = false;
            _context.Entry(Project).Property(m => m.PrjType).IsModified = false;
            _context.Entry(Project).Property(m => m.NewSpec).IsModified = false;
            if (PrjStage == 0)
            {
                _context.Entry(Project).Property(m => m.FreightStrat).IsModified = true;
            }
            else
            {
                _context.Entry(Project).Property(m => m.FreightStrat).IsModified = false;
            }

            if (PrjStage == 3)
            {
                _context.Entry(Project).Property(m => m.PrjPrice).IsModified = true;
            }
            else
            {
                _context.Entry(Project).Property(m => m.PrjPrice).IsModified = false;
            }

            if (PrjStage == 5)
            {
                _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = true;
            }
            else
            {
                _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = false;
            }

            Project.PrjStage = PrjStage;
            Project.PrjStage++;
            _context.Entry(Project).Property(m => m.PrjStage).IsModified = true;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.Id))
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

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
