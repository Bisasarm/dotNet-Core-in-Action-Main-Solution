﻿using Microsoft.EntityFrameworkCore;

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
        public async Task<RecipeDetailViewModel> GetRecipeDetail(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Recipes
                .Where(r => r.RecipeId == id)
                .Select(r => new RecipeDetailViewModel
                {
                    Name = r.Name,                    
                    Method = r.Method,
                    RecipeId = r.RecipeId,
                    Ingredients = r.Ingredients
                    .Select(item => new RecipeDetailViewModel.Item
                    {
                        Name = item.Name,
                        Quantity = $"Quantitiy: {item.Quantity}"
                    })
                })
                .SingleOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
