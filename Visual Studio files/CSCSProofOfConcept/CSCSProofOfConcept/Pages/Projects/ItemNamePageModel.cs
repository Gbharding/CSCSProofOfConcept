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

        public void PopulateItemDropDownList(CSCSProofOfConceptContext _context,
            object selectedItem = null)
        {
            var itemsQuery = from i in _context.Item
                             orderby i.Name
                             select i;
            ItemNameSL = new SelectList(itemsQuery.AsNoTracking(),
                nameof(Item.Id),
                nameof(Item.Name),
                selectedItem);
        }
    }
}
