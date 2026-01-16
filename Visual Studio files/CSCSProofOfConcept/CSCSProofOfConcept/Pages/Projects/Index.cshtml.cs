using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public IndexModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Project = await _context.Project
                .Include(i => i.Item)
                .ToListAsync();
        }
    }
}
