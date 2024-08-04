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
    [ValidateModelFilter(Order = -1)]
    [ExceptionFilter]
    public class FilteredRecipeAPIController : ControllerBase
    {
        private RecipeService _RecipeService { get; set; }
        public FilteredRecipeAPIController(RecipeService recipeService)
        {
            _RecipeService = recipeService;
        }
        [EnsureRecipeExistsFilter(Order = 0)]
        [HttpGet("nofilter/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_RecipeService.GetRecipe(id));
        }
        [EnsureRecipeExistsFilter(Order = 0)]
        [HttpPost("nofilter/{id}")]
        public IActionResult Edit(int id)
        {
            //Exception to test out exception
            //throw new Exception();
            return Ok(_RecipeService.EditRecipe(id));
        }

    }
}
