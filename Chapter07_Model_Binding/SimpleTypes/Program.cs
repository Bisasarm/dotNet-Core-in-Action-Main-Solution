using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//is supposed to be able to only interpret pxxxxx products with an integer
//this only works if the Product implements a tryparse -> results in it being accepted as a simple type!
//also added mandatory fromHeader to test a header read on the request
app.MapGet("/product/{id}", (ProductId id, [FromHeader]string testHeader) => $"Testheader{testHeader} and received product with ID: {id}");

app.MapPost("/JSONProduct", (JSONProduct? product) => $"Product bound to body: {product}");

//Query string includes multiple ids with the same name: ?id=123&id=1234

//this one has the crappy name "id" for multiple ids
//app.MapGet("/products", (int[] id) => $"There are {id.Length} ids in the URL ");
//this one has the better name for the query string parameter: id
app.MapGet("/products",
    ([FromQuery(Name = "id")] int[] ids) => $"received {ids.Length} ids");
//getting defaultalue does not yet work with lambda -> add it via declared method with a default value parameter
app.MapGet("/stock", StockWithDefaultValue);

app.MapPost("/size",(SizeDetails size) => $"sizes are {size}");

app.Run();

string StockWithDefaultValue(int id = 0) => $"received: {id}";

readonly record struct ProductId(int Id)
{
    //is run through automatically by the EndpointMiddleware while binding the parameters
    public static bool TryParse(string? s, out ProductId result)
    {
        if (s is not null 
            && s.ToLower().StartsWith('p')
            && int.TryParse(s.AsSpan().Slice(1), out int id))
        {
            result = new ProductId(id);
            return true;
        }
        result = default;
        return false;
    } 
}
record JSONProduct(int Id, string Name, int Stock);

//reads out the body the way it comes. no json, nothing
record SizeDetails(double Height, double Width)
{
    public static async ValueTask<SizeDetails> BindAsync(HttpContext context)
    {
        StreamReader sr = new StreamReader(context.Request.Body);

        //literally only reads the first line of the body
        string? s1 = await sr.ReadLineAsync(context.RequestAborted);
        if (s1 is null)
        {
            return null;
        }
        //literally only reads the second line of the body
        string? s2 = await sr.ReadLineAsync(context.RequestAborted);
        if (s2 is null)
        {
            return null;
        }
        if (double.TryParse(s1, out double r1)
            && double.TryParse(s2, out double r2))
        {
            return new SizeDetails(r1, r2);
        }
        else return null;
    }
}
