using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirportAuthorization.Pages.Airport
{
    [Authorize(policy: "CanEnterLounge")]
    public class AirportLoungeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
