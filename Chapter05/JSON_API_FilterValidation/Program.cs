using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var _fruit = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/", () => "Hello World!");

app.MapGet("/fruit/{id}", (string id) =>
    _fruit.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.Problem(statusCode: 404)
//The AddEndpointfilter-Method takes a delegate function with endpointfilterinvocationcontext and endpointfilterdelegate
).AddEndpointFilter(ValidationHelper.ValidateId);

app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
    _fruit.TryAdd(id, fruit)
        ? TypedResults.Created($"/fruit/{id}", fruit)
        : Results.ValidationProblem(new Dictionary<string, string[]> { { "id", new[] {"fruit id already exists"}} })
) ;

app.Run();

class ValidationHelper
{
    internal static async ValueTask<object> ValidateId(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var id = context.GetArgument<string>(0);
        if (String.IsNullOrEmpty(id) || !id.StartsWith('f'))
        {
            return Results.ValidationProblem(new Dictionary<string, string[]> {
                { "id", new[] { "the id was empty or must start with an f" } }
            });
        }
        return await next(context);
    }

}

public record Fruit(string Name, int Stock);