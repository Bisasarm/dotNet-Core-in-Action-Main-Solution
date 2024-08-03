using Microsoft.AspNetCore.Mvc;

namespace RecipeWebAPI.Controllers
{
    public class RecipeAPIController : ControllerBase
    {
        private RecipeService _RecipeService { get; set; }
        public RecipeAPIController(RecipeService recipeService)
        {
            _RecipeService = recipeService;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
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
        [HttpPost("{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
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
