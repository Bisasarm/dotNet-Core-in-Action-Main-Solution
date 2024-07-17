namespace EFCore
{
    public class CreateRecipeCommand : EditRecipeBase
    {
        public List<CreateIngredientCommand> Ingredients { get; set; } = new List<CreateIngredientCommand>();
        public Recipe ToRecipe()
        {
            return new Recipe
            {
                Name = Name,
                CookingTime = new TimeSpan(CookingTimeHrs, CookingTimeMins, 0),
                Method = Method,
                IsVegan = IsVegan,
                IsDeleted = IsVegetarian,
                Ingredients = Ingredients.Select(x => x.CreateIngredient()).ToList()
            };
        }
    }
}