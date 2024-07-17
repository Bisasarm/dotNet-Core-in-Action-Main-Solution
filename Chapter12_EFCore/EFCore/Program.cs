using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
//adding the AppDbContext as a service to the DI Container
//also adding the DBContextOptionsBuilder with the corresponding DB provider and the connection string
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connString));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Recipe> Recipes { get; set; }    
}

class Recipe
{
    public int RecipeId { get; set; }
    public required string Name { get; set; }
    public bool IsDeleted { get; set; }
    public required string Method { get; set; }
    public TimeSpan CookingTime { get; set; }

    public required ICollection<Ingredient> Ingredients { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
}

class Ingredient
{
    public int IngredientId { get; set; }
    public int RecipeId { get; set; }
    public required string Name { get; set; }
    public decimal Quantity { get; set; }
    public required string Unit { get; set; }
}