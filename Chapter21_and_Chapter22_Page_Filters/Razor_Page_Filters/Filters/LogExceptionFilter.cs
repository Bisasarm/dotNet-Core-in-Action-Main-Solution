using Microsoft.AspNetCore.Mvc.Filters;

namespace Razor_Page_Filters.Filters
{
    /// <summary>
    /// Does not catch exceptions before an action
    /// </summary>
    public class LogExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"executed on exception:{context.Exception.Message}");
        }
    }
}
