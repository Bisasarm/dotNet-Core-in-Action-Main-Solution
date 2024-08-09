using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AirportAuthorization.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel(UserManager<IdentityUser> userManager)
        {
            
        }

        public void OnGet()
        {

        }


    }
}
