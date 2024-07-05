using System.Collections.Concurrent;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ConcurrentDictionary<string, Fruit> _fruit = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/", () => "Hello World!");

RouteGroupBuilder fruitAPI = app.MapGroup("/fruit");

fruitAPI.MapGet("/", () => _fruit);

RouteGroupBuilder fruitAPIWithValidation = fruitAPI.MapGroup("/{id}").AddEndpointFilterFactory(ValidationHelper.ValidateIdFactory);

fruitAPIWithValidation.MapGet("/", (string id) =>
    _fruit.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.Problem(statusCode: 404));

app.Run();

public record Fruit(string Name, int Stock);
class ValidationHelper
{
    internal static EndpointFilterDelegate ValidateIdFactory(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        ParameterInfo[] idParameters = context.MethodInfo.GetParameters();
        int? idPosition = null;
        for (int i = 0; i < idParameters.Length; i++)
        {
            if (idParameters[i].Name == "id" && idParameters[i].ParameterType == typeof(string))
            {
                idPosition = i;
                break;
            }
        }
        if (!idPosition.HasValue)
        {
            return next;
        }
        return async (invocationContext) =>
        {
            string id = invocationContext.GetArgument<string>(idPosition.Value);
            if (string.IsNullOrEmpty(id) || !id.StartsWith("f"))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]> { { "id", new[] { "id must start with f and this was invoked by a endpointfilterfactory" } } });
            }
            return await next(invocationContext);
        };
    } 
}