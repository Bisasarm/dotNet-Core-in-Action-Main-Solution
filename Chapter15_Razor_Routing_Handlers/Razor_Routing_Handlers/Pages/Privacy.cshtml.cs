using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_Routing_Handlers.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        //public required string Argument { get; set; }

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            //this should redirect to index on page
            return RedirectToPage("/");
        }
        //public IActionResult OnPost(string test)
        //{
        //    Console.WriteLine(test);
        //    return Page();
        //}
        //public void OnPut()
        //{
        //    Console.WriteLine("Argument");
        //}
    }

}
