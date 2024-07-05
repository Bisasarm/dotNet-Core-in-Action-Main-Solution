using System.Collections.Concurrent;
using System.Reflection;
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
).AddEndpointFilterFactory(ValidationHelper.ValidateIdFactory)
.AddEndpointFilter(async (context, next) =>
{
    app.Logger.LogInformation("Executing logging filter..");
    object? result = await next(context);
    app.Logger.LogInformation($"Handler result:  {result}");
    return result;
});

//alternative get method to test interface implemented endpointfilter
app.MapGet("/fruit/test/{id}", (string id) =>
    _fruit.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.Problem(statusCode: 404))
    .AddEndpointFilter<IdValidationFilter>();


//With EndpointFilterFactory
app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
    _fruit.TryAdd(id, fruit)
        ? TypedResults.Created($"/fruit/{id}", fruit)
        : Results.ValidationProblem(new Dictionary<string, string[]> { { "id", new[] { "fruit id already exists" } } })
).AddEndpointFilterFactory(ValidationHelper.ValidateIdFactory);


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
            //Shortcircuiting the endpoint filter pipeline
            return Results.ValidationProblem(new Dictionary<string, string[]> {
                { "id", new[] { "the id was empty or must start with an f" } }
            });
        }
        return await next(context);
    }
    internal static EndpointFilterDelegate ValidateIdFactory(
        EndpointFilterFactoryContext context,
        EndpointFilterDelegate next)
    {
        ParameterInfo[] parameters = context.MethodInfo.GetParameters();
        int? idPosition = null;
        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[0].Name.Equals("id") && parameters[0].ParameterType == typeof(string))
            {
                idPosition = i;
                break;
            }
        }
        //if the id parameter is not found it will not add the filter but the rest of the pipeline will be added
        if (!idPosition.HasValue)
        {
            return next;
        }
        //instead of giving back a delegate as next, we will create our own method
        //the invocationContext is implicit of the endpointfilterinvocationcontext type
        //this is due to the expected return value, which is a function
        return async (invocationContext) =>
        {
            var id = invocationContext.GetArgument<string>(idPosition.Value);
            if (string.IsNullOrEmpty(id) || !id.StartsWith("f"))
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        {
                            "id",
                            new[]{"id has to start with f. This is the factory filter"}
                        }
                    }
                );
            }
            return await next(invocationContext);
        };
    }
}
internal class IdValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string id = context.GetArgument<string>(0);
        if (string.IsNullOrEmpty(id) || !id.StartsWith("f"))
        {
            return Results.ValidationProblem(
                new Dictionary<string, string[]>
                {
                        { "id", new[]{ "id must start with f. This is the implemented Endpointfilter Interface" } }
                });
        }
        return await next(context);
    }
}

public record Fruit(string Name, int Stock);