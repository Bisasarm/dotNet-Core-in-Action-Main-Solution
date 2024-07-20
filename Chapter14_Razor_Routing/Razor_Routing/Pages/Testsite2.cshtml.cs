using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_Routing.Pages
{
    public class Testsite2Model : PageModel
    {
        public string LinkToTestsite { get; set; } = string.Empty;
        public ActionResult OnGet()
        {
            LinkToTestsite = Url.Page("testsite", new { someparameter = "1", someparameter2 = "2"});
            return Page();
        }
    }
}
