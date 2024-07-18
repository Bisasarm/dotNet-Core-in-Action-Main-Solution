namespace EFCore
{/// <summary>
/// Class with all of the properties of editrecipebase
/// Has an additional ID property to find the corresponding DB entry
/// Has the update method, which uses call by ref to update the passed recipe
/// </summary>
    public class UpdateRecipeCommand:EditRecipeBase
    {
        public int Id { get; set; }
        public void UpdateRecipe(Recipe recipe)
        {
            recipe.Name = Name;
            recipe.Method = Method;
            recipe.CookingTime = new TimeSpan(CookingTimeHrs, CookingTimeMins, 0);
            recipe.IsVegan = IsVegan;
            recipe.IsVegetarian = IsVegetarian;
        }
    }
}