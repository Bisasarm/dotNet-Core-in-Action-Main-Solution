namespace RecipeWebAPI
{
    public class RecipeService
    {
        public string GetRecipe(int id)
        {
            return $"Recipe No. {id}";
        }
        public string EditRecipe(int id)
        {
            return $"Edited Recipe No. {id}";
        }
        public bool DoesRecipeExist(int id)
        {
            return true;
        }
    }
}
