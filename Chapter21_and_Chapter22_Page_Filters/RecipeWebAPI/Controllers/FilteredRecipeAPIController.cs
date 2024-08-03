using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebAPI.Filters;

namespace RecipeWebAPI.Controllers
{
    //testing out authorizration
    /// <summary>
    /// Refactored class of RecipeAPIController
    /// </summary>
    [ValidateFeatureFlag(Enabled = true)]
    [ValidateModelFilter]
    public class FilteredRecipeAPIController : ControllerBase
    {
        private RecipeService _RecipeService { get; set; }
        public FilteredRecipeAPIController(RecipeService recipeService)
        {
            _RecipeService = recipeService;
        }
        [HttpGet("nofilter/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (!_RecipeService.DoesRecipeExist(id))
                {
                    return NotFound();
                }
                return Ok(_RecipeService.GetRecipe(id));
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }


        }
        [HttpPost("nofilter/{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                if (!_RecipeService.DoesRecipeExist(id))
                {
                    return NotFound();
                }
                //Exception to test out exception
                //throw new Exception();
                return Ok(_RecipeService.EditRecipe(id));
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }
        private static IActionResult GetErrorResult(Exception ex)
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
