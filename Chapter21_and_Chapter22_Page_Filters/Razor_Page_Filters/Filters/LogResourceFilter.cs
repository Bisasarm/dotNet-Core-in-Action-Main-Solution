using Microsoft.AspNetCore.Mvc.Filters;

namespace Razor_Page_Filters.Filters
{
    public class LogResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //Should execute on the way back after the rest of the filter pipeline was executed
            Console.WriteLine("2. Resource Filter executed");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //should execute before the Page handler selection
            Console.WriteLine("1. Resource Filter executing");
        }
    }
}
