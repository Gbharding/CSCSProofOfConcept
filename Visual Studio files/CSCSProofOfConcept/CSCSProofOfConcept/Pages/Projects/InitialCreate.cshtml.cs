using CSCSProofOfConcept.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSCSProofOfConcept.Pages.Projects
{
    public class InitialCreateModel : PageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public InitialCreateModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPostAsync(int type)
        {
            CacheData.InProgPrj = type;
            return RedirectToPage("./Create");
        }
    }
}
