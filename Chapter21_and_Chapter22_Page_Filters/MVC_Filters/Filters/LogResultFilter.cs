using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_Filters.Filters
{
    public class LogResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("5. This is executed after the IActionResult has been executed");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("4. This is executed before the IActionResult has been executed");
        }
    }
}
