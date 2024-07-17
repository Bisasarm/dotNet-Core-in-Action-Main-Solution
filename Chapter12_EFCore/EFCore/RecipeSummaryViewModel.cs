namespace EFCore
{

    public class RecipeSummaryViewModel
    {
        public required string Name { get; set; }
        public int Id { get; set; }
        public required string TimeToCook { get; set; }
        public int NumberIngredients { get; set; }
        //public RecipeSummaryViewModel FromRecipe(Recipe recipe)
        //{
        //    return new RecipeSummaryViewModel
        //    {
        //        Name = recipe.Name,
        //        Id = recipe.RecipeId,
        //        NumberIngredients = recipe.Ingredients.Count,
        //        TimeToCook = $"Hours: {recipe.CookingTime.Hours} Mins: {recipe.CookingTime.Minutes}"
        //    };
        //}
    }
}