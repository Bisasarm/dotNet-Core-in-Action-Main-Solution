﻿namespace EFCore
{
    public class RecipeDetailViewModel
    {
        public int RecipeId { get; set; }
        public required string Name { get; set; }
        public required string Method { get; set; }
        public required IEnumerable<Item> Ingredients { get; set; }
        public class Item
        {
            public required string Name { get; set; }
            public required string Quantity { get; set; }
        }
    }


}