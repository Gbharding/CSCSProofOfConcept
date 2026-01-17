using CSCSProofOfConcept.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSCSProofOfConcept.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CSCSProofOfConcept.Data.CSCSProofOfConceptContext _context;

        public IndexModel(CSCSProofOfConcept.Data.CSCSProofOfConceptContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            CacheData.User = "Logout";
        }
        public async Task<IActionResult> OnPostAsync(string user)
        {
            CacheData.User = user;
            return RedirectToPage("./Projects/Index");
        }
    }
}
