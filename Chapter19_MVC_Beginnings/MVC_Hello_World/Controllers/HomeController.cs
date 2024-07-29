using Microsoft.AspNetCore.Mvc;
using MVC_Hello_World.Models;
using System.Diagnostics;

namespace MVC_Hello_World.Controllers
{
    public class HomeController : Controller
    {
        //due to conventional routing it has multiple endpoints for the same routing template
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //runs in response to a request of /home/ or home/index
        public IActionResult Index()
        {
            //renders a Razor-View
            //actually renders the view in views/{controller=home in this case}/{action = index in this case according to methodname}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //Filters?
        //This is called if there is a redirection to home/error as in program.cs
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //creates a view with the viewmodel "ErrorViewModel", which has the given properties
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
