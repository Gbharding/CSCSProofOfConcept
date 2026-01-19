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

        public async Task<IActionResult> OnPostAsync(int type)
        {
            //Approved
            if (type == 1)
            {
                _context.Attach(Project).State = EntityState.Modified;
                _context.Entry(Project).Property(m => m.DueDate).IsModified = false;
                _context.Entry(Project).Property(m => m.Id).IsModified = false;
                _context.Entry(Project).Property(m => m.Name).IsModified = false;
                _context.Entry(Project).Property(m => m.Description).IsModified = false;
                _context.Entry(Project).Property(m => m.ItemId).IsModified = false;
                _context.Entry(Project).Property(m => m.PrjType).IsModified = false;
                _context.Entry(Project).Property(m => m.NewSpec).IsModified = false;
                if (StaticProject.PrjStage == 0)
                {
                    _context.Entry(Project).Property(m => m.FreightStrat).IsModified = true;
                }
                else
                {
                    _context.Entry(Project).Property(m => m.FreightStrat).IsModified = false;
                }

                if (StaticProject.PrjStage == 3)
                {
                    _context.Entry(Project).Property(m => m.PrjPrice).IsModified = true;
                }
                else
                {
                    _context.Entry(Project).Property(m => m.PrjPrice).IsModified = false;
                }

                if (StaticProject.PrjStage == 5)
                {
                    _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = true;
                }
                else
                {
                    _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = false;
                }

                if (StaticProject.PrjStage == 0 && StaticProject.PrjType != PrjType.New)
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage += 3;
                }
                else if (StaticProject.PrjStage == 4 && StaticProject.PrjType == PrjType.Discontinue)
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage += 2;
                }
                else if (StaticProject.PrjStage == 6 && StaticProject.PrjType != PrjType.New)
                {
                    if (StaticProject.PrjType == PrjType.Discontinue)
                    {
                        Project.PrjStage = StaticProject.PrjStage;
                        Project.PrjStage += 2;
                    }
                    else
                    {
                        Project.PrjStage = StaticProject.PrjStage;
                        Project.PrjStage += 3;
                    }
                }
                else if (StaticProject.PrjStage == 7)
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage += 2;
                }
                else
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage++;
                }
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


                Project.History = StaticProject.History;
                Project.History += CacheData.User + " Accepted " + DateTime.Now.ToString() + "; ";
                _context.Entry(Project).Property(m => m.History).IsModified = true;


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
            else //Rejected
            {
                _context.Attach(Project).State = EntityState.Modified;
                _context.Entry(Project).Property(m => m.DueDate).IsModified = false;
                _context.Entry(Project).Property(m => m.Id).IsModified = false;
                _context.Entry(Project).Property(m => m.Name).IsModified = false;
                _context.Entry(Project).Property(m => m.Description).IsModified = false;
                _context.Entry(Project).Property(m => m.ItemId).IsModified = false;
                _context.Entry(Project).Property(m => m.PrjType).IsModified = false;
                _context.Entry(Project).Property(m => m.NewSpec).IsModified = false;
                _context.Entry(Project).Property(m => m.FreightStrat).IsModified = false;
                _context.Entry(Project).Property(m => m.PrjPrice).IsModified = false;
                _context.Entry(Project).Property(m => m.DistributionCenterId).IsModified = false;

                if (StaticProject.PrjStage == 3 && StaticProject.PrjType != PrjType.New)
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage -= 3;
                } else if (StaticProject.PrjStage == 6 && StaticProject.PrjType == PrjType.Discontinue)
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage -= 2;
                } else if (StaticProject.PrjStage == 8)
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage -= 2;
                } else if (StaticProject.PrjStage == 9)
                {
                    switch (StaticProject.PrjType)
                    {
                        case PrjType.New:
                            Project.PrjStage = StaticProject.PrjStage;
                            Project.PrjStage -= 2;
                            break;
                        case PrjType.Transition:
                            Project.PrjStage = StaticProject.PrjStage;
                            Project.PrjStage -= 3;
                            break;
                        case PrjType.Discontinue:
                            Project.PrjStage = StaticProject.PrjStage;
                            Project.PrjStage--;
                            break;
                    }
                }
                else
                {
                    Project.PrjStage = StaticProject.PrjStage;
                    Project.PrjStage--;
                }

                _context.Entry(Project).Property(m => m.PrjStage).IsModified = true;

                Project.History = StaticProject.History;
                Project.History += CacheData.User + " Rejected " + DateTime.Now.ToString() + "; ";
                _context.Entry(Project).Property(m => m.History).IsModified = true;

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
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
