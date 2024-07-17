namespace EFCore
{
    public class EditRecipeBase
    {
        public required string Name { get; set; }
        public required string Method { get; set; }
        public int CookingTimeHrs { get; set; }
        public int CookingTimeMins { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
    }
}