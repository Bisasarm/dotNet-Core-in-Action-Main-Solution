using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_Filters.Filters
{
    public class LogResourceFilter : Attribute, IResourceFilter
    {
        void IResourceFilter.OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("6. this should be the last filter");
        }

        void IResourceFilter.OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("1. this should be the first filter");
        }
    }
}
