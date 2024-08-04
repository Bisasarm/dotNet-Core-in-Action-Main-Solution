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
    [ExceptionFilter]
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
            if (!_RecipeService.DoesRecipeExist(id))
            {
                return NotFound();
            }
            return Ok(_RecipeService.GetRecipe(id));
        }        
        [HttpPost("nofilter/{id}")]
        public IActionResult Edit(int id)
        {
            if (!_RecipeService.DoesRecipeExist(id))
            {
                return NotFound();
            }
            //Exception to test out exception
            throw new Exception();
            return Ok(_RecipeService.EditRecipe(id));
        }

    }
}
