using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_View.Pages
{
    public class ToDoListModel : PageModel
    {
        public List<string> Tasks = new List<string> { "task1", "task2", "task3" };
        public void OnGet()
        {
        }
    }
}
