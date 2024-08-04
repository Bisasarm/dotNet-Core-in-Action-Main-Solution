using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RecipeWebAPI.Filters
{
    public class EnsureRecipeExistsFilter: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            RecipeService? recipeService = (RecipeService?)context.HttpContext.RequestServices.GetService(typeof(RecipeService));
            if (recipeService is not null)
            {
                int? id = (int?)context.ActionArguments["id"];
                if (id is null)
                {
                    context.Result = new NotFoundResult();
                }

            }
        }
    }
}
