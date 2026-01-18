using CSCSProofOfConcept.Data;
using CSCSProofOfConcept.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CSCSProofOfConcept.Pages.Projects
{
    public class ItemNamePageModel : PageModel
    {
        public SelectList ItemNameSL {  get; set; }
        public SelectList DCNameSL { get; set; }

        public void PopulateItemDropDownList(CSCSProofOfConceptContext _context,
            object selectedItem = null)
        {
            var itemsQuery = from i in _context.Item
                             orderby i.Name
                             where i.IsActive == true
                             select i;
            ItemNameSL = new SelectList(itemsQuery.AsNoTracking(),
                nameof(Item.Id),
                nameof(Item.Name),
                selectedItem);
        }

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
