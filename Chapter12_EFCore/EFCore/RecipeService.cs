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
    }
}
