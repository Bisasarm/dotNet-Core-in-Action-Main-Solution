using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RecipeWebAPI.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = GetErrorResult(context.Exception);
            context.ExceptionHandled = true;
        }
        private IActionResult GetErrorResult(Exception ex)
        {
            var error = new ProblemDetails
            {
                Status = 500,
                Title = "A custom error occured",
                Detail = ex.Message,
                Type = "https://httpstatuses.com/500"
            };
            return new ObjectResult(error);
        }
    }
}
