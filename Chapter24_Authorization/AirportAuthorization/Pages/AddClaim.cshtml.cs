using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AirportAuthorization.Pages
{
    [Authorize]
    public class AddClaimModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        //[BindProperty]
        //public InputModel Input { get; set; }
        public AddClaimModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGet(string claimName, string claimValue)
        {
            var user = await _userManager.GetUserAsync(User);
            var claim = new Claim(claimName, claimValue);

            if (user is not null)
            {
                await _userManager.AddClaimAsync(user, claim);
                Console.WriteLine($"Claim: {claimName}, {claimValue} for user {user.UserName} sucessfully added");
            }
        }
        //public record InputModel(string Claim, string ClaimValue);
    }
}
