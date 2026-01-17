using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;

namespace CSCSProofOfConcept.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public IndexModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; } = default!;
        public string User {  get; set; } = default!;

        public async Task OnGetAsync()
        {
            Item = await _context.Item
                .Include(c => c.DistributionCenter)
                .AsNoTracking()
                .ToListAsync();
            User = CacheData.User;
        }
    }
}
