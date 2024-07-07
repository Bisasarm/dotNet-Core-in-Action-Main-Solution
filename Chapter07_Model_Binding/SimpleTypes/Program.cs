using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//is supposed to be able to only interpret pxxxxx products with an integer
//this only works if the Product implements a tryparse -> results in it being accepted as a simple type!
app.MapGet("/product/{id}", (ProductId id, [FromHeader]string testHeader) => $"Testheader{testHeader} and received product with ID: {id}");

app.Run();


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