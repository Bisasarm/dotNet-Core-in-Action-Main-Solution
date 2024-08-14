using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_Logging.Pages
{
    public class IndexModel : PageModel
    {
        //logger has already been established as property
        private readonly ILogger<IndexModel> _logger;

        //ILogger is expected to be injected via DI
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("Index page has been called via {Verb}", "get");
        }
    }
}
