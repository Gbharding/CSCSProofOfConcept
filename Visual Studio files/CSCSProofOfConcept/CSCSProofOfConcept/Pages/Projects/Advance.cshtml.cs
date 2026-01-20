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
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public static Project StaticProject { get; set; } = default!;

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
            StaticProject = project;

            PopulateDCDropDownList(_context);
            return Page();
        }

        public async Task<IActionResult> OnPostAcceptAsync(int? id)
        {
            //Someone tried to advance without adding a DC on a new project- this would create a new item with no DC,
            //so don't let this happen.
            if (Project.DistributionCenterId == null && StaticProject.PrjType == PrjType.New && StaticProject.PrjStage == 5)
            {
                Project = _context.Project.Where(p => p.Id == id).First();
                return RedirectToRoute(new { id = Project.Id });
            }

            //Enter all the affected items by the current stage
            EnterNotModified(false,StaticProject.PrjStage);

            //Some stage, project type, and action combinations require a different amount of steps than others
            int skipSteps = SkipSteps(false, StaticProject.PrjStage, StaticProject.PrjType);

            Project.PrjStage = StaticProject.PrjStage;
            Project.PrjStage += skipSteps;
            _context.Entry(Project).Property(m => m.PrjStage).IsModified = true;

            //Project is finished, publish changes
            if (Project.PrjStage == 10)
            {
                switch (StaticProject.PrjType)
                {
                    case PrjType.New:
                        var emptyItem = new Item();

                        emptyItem.IsActive = true;
                        emptyItem.Name = "Item " + StaticProject.Name;
                        if (String.IsNullOrEmpty(StaticProject.NewSpec))
                        {
                            emptyItem.Specifications = "No specifications given";
                        }
                        else
                        {
                            emptyItem.Specifications = StaticProject.NewSpec;
                        }
                        emptyItem.DistributionCenterId = (int)StaticProject.DistributionCenterId;
                        _context.Item.Add(emptyItem);
                        break;
                    case PrjType.Transition:
                        var item = await _context.Item
                        .FirstOrDefaultAsync(m => m.Id == StaticProject.ItemId);
                        _context.Attach(item).State = EntityState.Modified;
                        _context.Entry(item).Property(m => m.Id).IsModified = false;
                        _context.Entry(item).Property(m => m.Name).IsModified = false;
                        if (!String.IsNullOrEmpty(StaticProject.NewSpec))
                        {
                            item.Specifications = StaticProject.NewSpec;
                            _context.Entry(item).Property(m => m.Specifications).IsModified = true;
                        }
                        else
                        {
                            _context.Entry(item).Property(m => m.Specifications).IsModified = false;
                        }

                        if (StaticProject.DistributionCenterId != null)
                        {
                            item.DistributionCenterId = (int)StaticProject.DistributionCenterId;
                            _context.Entry(item).Property(m => m.DistributionCenterId).IsModified = true;
                        }
                        else
                        {
                            _context.Entry(item).Property(m => m.DistributionCenterId).IsModified = false;
                        }
                        break;
                    case PrjType.Discontinue:
                        item = await _context.Item
                        .FirstOrDefaultAsync(m => m.Id == StaticProject.ItemId);
                        _context.Attach(item).State = EntityState.Modified;
                        _context.Entry(item).Property(m => m.Id).IsModified = false;
                        _context.Entry(item).Property(m => m.Name).IsModified = false;
                        _context.Entry(item).Property(m => m.Specifications).IsModified = false;
                        _context.Entry(item).Property(m => m.DistributionCenterId).IsModified = false;
                        item.IsActive = false;
                        _context.Entry(item).Property(m => m.IsActive).IsModified = true;

                        break;
                }

            }

            //Add the action to the Project history
            LogHistory(false);

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

        public async Task<IActionResult> OnPostRejectAsync()
        {
            //Enter all the affected items by the current stage
            EnterNotModified(true,0);

            //Some stage, project type, and action combinations require a different amount of steps than others
            int skipSteps = SkipSteps(true, StaticProject.PrjStage, StaticProject.PrjType);

            Project.PrjStage = StaticProject.PrjStage;
            Project.PrjStage -= skipSteps;
            _context.Entry(Project).Property(m => m.PrjStage).IsModified = true;

            //Add the action to the Project history
            LogHistory(true);

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

        public void EnterNotModified(bool isReject, int prjStage)
        {
            _context.Attach(Project).State = EntityState.Modified;
            _context.Entry(Project).Property(m => m.DueDate).IsModified = false;
            _context.Entry(Project).Property(m => m.Id).IsModified = false;
            _context.Entry(Project).Property(m => m.Name).IsModified = false;
            _context.Entry(Project).Property(m => m.Description).IsModified = false;
            _context.Entry(Project).Property(m => m.ItemId).IsModified = false;
            _context.Entry(Project).Property(m => m.PrjType).IsModified = false;
            _context.Entry(Project).Property(m => m.NewSpec).IsModified = false;
            if (isReject)
            {
                _context.Entry(Project).Property(m => m.FreightStrat).IsModified = false;
                _context.Entry(Project).Property(m => m.PrjPrice).IsModified = false;
                _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = false;
            } else
            {
                _context.Entry(Project).Property(m => m.FreightStrat).IsModified = (prjStage == 0);
                _context.Entry(Project).Property(m => m.PrjPrice).IsModified = (prjStage == 3);
                _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = (prjStage == 5);
            }
        }

        public int SkipSteps(bool isReject, int prjStage, PrjType prjType)
        {
            if (prjStage == 6 && prjType == PrjType.Discontinue)
            {
                return 2;
            }
            if (prjStage == 7 && isReject == false)
            {
                return 2;
            }
            if (prjStage == 8 && isReject == true)
            {
                return 2;
            }
            if (prjStage == 0 && prjType != PrjType.New && isReject == false)
            {
                return 3;
            }
            if (prjStage == 3 && prjType != PrjType.New && isReject == true)
            {
                return 3;
            }
            if (prjStage == 9 && prjType == PrjType.New && isReject == true)
            {
                return 2;
            }
            if (prjStage == 9 && prjType == PrjType.Transition && isReject == true)
            {
                return 3;
            }
            if (prjStage == 4 && prjType == PrjType.Discontinue && isReject == false)
            {
                return 2;
            }
            if (prjStage == 6 && prjType == PrjType.Transition && isReject == false)
            {
                return 3;
            }

            return 1;
        }

        public void LogHistory(bool isReject)
        {
            string action;
            if (isReject)
            {
                action = " Rejected ";
            } else
            {
                action = " Accepted ";
            }
            Project.History = StaticProject.History;
            Project.History += "\n" + CacheData.User + action + DateTime.Now.ToString();
            _context.Entry(Project).Property(m => m.History).IsModified = true;
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
