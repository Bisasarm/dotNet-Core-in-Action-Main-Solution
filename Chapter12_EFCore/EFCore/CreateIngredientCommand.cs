namespace EFCore
{
    public class CreateIngredientCommand
    {
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public required string Unit { get; set; }
        public Ingredient CreateIngredient()
        {
            return new Ingredient
            {
                Name = Name,
                Quantity = Quantity,
                Unit = Unit
            };
        }
    }
}