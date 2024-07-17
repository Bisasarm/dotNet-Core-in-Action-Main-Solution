using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class RecipeService
    {
        readonly AppDbContext _dbContext;
        public RecipeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateRecipe(CreateRecipeCommand cmd)
        {
            var recipe = cmd.ToRecipe();
            _dbContext.Add(recipe);
            await _dbContext.SaveChangesAsync();
            return recipe.RecipeId;
        }
        public async Task<ICollection<RecipeSummaryViewModel>> GetRecipes()
        {
            return await _dbContext.Recipes
                .Where(r => !r.IsDeleted)
                .Select(x => new RecipeSummaryViewModel
                {
                    Id = x.RecipeId,
                    Name = x.Name,
                    NumberIngredients = x.Ingredients.Count,
                    TimeToCook = $"Hrs: {x.CookingTime.Hours} Mins {x.CookingTime.Minutes}"
                }).ToListAsync();                
        }
    }
}
