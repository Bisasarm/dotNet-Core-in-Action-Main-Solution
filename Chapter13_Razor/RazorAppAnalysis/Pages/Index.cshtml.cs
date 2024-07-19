using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorAppAnalysis.Pages
{
    //the corresponding class to the page MUST inherit from PageModel
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //constructor which can be filled with dependency injection/services
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        //Method called by endpoint middleware on get request
        public void OnGet()
        {

        }
    }
}
