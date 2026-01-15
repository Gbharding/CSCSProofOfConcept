using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CSCSProofOfConcept.Pages.Items
{
    public class DCNamePageModel : PageModel

    {
        public SelectList DCNameSL {  get; set; }

        public void PopulateDCDropDownList(CSCSProofOfConceptContext _context,
            object selectedDC = null)
        {
            var dcsQuery = from d in _context.DistributionCenter
                           orderby d.Name
                           select d;

            DCNameSL = new SelectList(dcsQuery.AsNoTracking(),
                nameof(DistributionCenter.Id),
                nameof(DistributionCenter.Name),
                selectedDC);
        }
    }
}
