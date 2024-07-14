using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//Middleware Exposes OpenAPI Document for the app
// can be called via /swagger/v1/swagger.json
app.UseSwagger();
//Middleware to serves the OpenAPI Document to SwaggerUI
//can be called via /swagger
app.UseSwaggerUI();
var _list = new ConcurrentDictionary<string, Fruit>();

app.MapGet("/", () => "Hello World!");
app.MapGet("/fruit/{id}", (string id) =>
{
    return _list.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.Problem(statusCode: 404);
});
app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
    _list.TryAdd(id, fruit)
        ? TypedResults.Created($"created fruit/{id}", fruit)
        : Results.ValidationProblem(new Dictionary<string, string[]> { { "id", new[] { "id is already being used" } } })
);

app.Run();

record Fruit(string Name, int Stock);