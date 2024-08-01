using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_Filters.Filters
{
    public class LogActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("3. Action has been executed: Only for /Privacy");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("2. Action is about to execute: Only for /Privacy");
        }
    }
}
