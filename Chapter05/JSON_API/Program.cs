var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List <Person> people = new List<Person>
{
    new Person("Tom", "Hanks"),
    new Person("Al", "Pacino"),
    new Person("Salome", "Jöris"),
    new Person("Alexis", "Sarmiento")
};

app.MapGet("/", () => "Hello World!");
//Endpoint with a parameter
app.MapGet("/person/{name}", (string name) =>
    //Parameter is being used to lambda search through a list
    people.Where(p => p.firstName.StartsWith(name)));
//return all fruit from the Fruit.All Dictionary
app.MapGet("/fruit", () => Fruit.All);

//set up a lambda as function expression to reuse later
Func<string, Fruit> lambdaSearchFruit = (string id) => Fruit.All[id];
//use the stored lambda expression to get specific fruit
app.MapGet("/fruit/{id}", lambdaSearchFruit);
//adding fruit via JSON and STATIC method for adding fruit
app.MapPost("/fruit/{id}", Handlers.AddFruit);
//replacing fruit via JSON and NOT STATIC method for replacing fruit
//first the handler must be initialized for a non static to work
Handlers handler = new();
app.MapPut("/fruit/{id}", handler.ReplaceFruit);
//Local methods can also be used
//Deletefruit only needs the ID, simple
app.MapDelete("/fruit/{id}", DeleteFruit);

app.Run();
//Local Method to delete Fruit dictionary entry for currently existing fruit
void DeleteFruit(string id)
{
    Fruit.All.Remove(id);
}

/// <summary>
/// Creating a record with the name Fruit which has a name and a stock
/// </summary>
/// <param name="Name"></param>
/// <param name="Stock"></param>
record Fruit(string Name, int Stock)
{
    //this invokes the creation of a dictionary with the name all. It will have all of the current fruit
    public static readonly Dictionary<string, Fruit> All = new();
};
record Person(string firstName, string lastName);

/// <summary>
/// Handlers for removing and adding fruit
/// </summary>
class Handlers
{
    /// <summary>
    /// Non static. Handler must be initialized beforehand
    /// Binding of parameters works because the .NET Core Framework matches the parameters in the url {id} and the string id
    /// This is case sensitive
    /// Explicit binding is also possible
    /// The binding of the JSON Body to Fruit it also done automatically
    /// </summary>
    /// <param name="id">needed to have a dictionary key</param>
    /// <param name="fruit">needed to give a whole fruit record</param>
    public void ReplaceFruit(string id, Fruit fruit)
    {
        Fruit.All[id] = fruit;
    }
    /// <summary>
    /// static. Handler does not need to be initialized beforehand
    /// </summary>
    /// <param name="id">needed to have a new id for dictionary key</param>
    /// <param name="fruit">needed to add a fruit record</param>
    public static void AddFruit(string id ,Fruit fruit)
    {
        Fruit.All.Add(id, fruit);
    }
}