using System.Collections.Concurrent;
using System.Net.Mime;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
//adding the problemdetails to exceptions
//this results in a json formatted webstandard response for errorhandling
builder.Services.AddProblemDetails();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    //this time without a path to invoke the IProblemDetailsService
    app.UseExceptionHandler();
}
//add problem details to 404 not found endpoints
app.UseStatusCodePages();

app.MapGet("/exception", void () => throw new Exception("test exception"));
app.MapGet("/", () => "Hello World!");

//thread safe dictionary. uses more space and is performance heavier than a normal one
var _fruit = new ConcurrentDictionary<string, Fruit>();

//just the endpoint to get all of the fruit
app.MapGet("/fruit", () => _fruit);

//get a specific fruit and handle a not found with 404
//added problem for more details
app.MapGet("/fruit/{id}", (string id) =>
{
    if (string.IsNullOrEmpty(id) || !id.StartsWith('f'))
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "id needs to start with f" } }
        });
    }
    else
    {
        return _fruit.TryGetValue(id, out var fruit)
            ? TypedResults.Ok(fruit)
            : Results.Problem(statusCode: 404);
    }
});
//handle an already created fruit with a message and 400
//validationproblem added to add more details. It needs a dictionary with the message inside of it
app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
    _fruit.TryAdd(id,fruit)
        ? TypedResults.Created(id, fruit)
        : Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "id", new[] { "A fruit with this id has already been created", "teststring" } },
            { "test", new[] {"This won't be shown" } }            
        }));
//handle an update always with 204 no content
app.MapPut("/fruit/{id}", (string id, Fruit fruit) =>
{
    _fruit[id] = fruit;
    return Results.NoContent();
});
//handle a delete always with 204 no content
app.MapDelete("fruit/{id}", (string id) =>
{
    _fruit.TryRemove(id, out _);
    return Results.NoContent();
});
//manual response generation for the joke code 418
app.MapGet("/teapot", (HttpResponse response) =>
{
    //setting the response properties
    response.StatusCode = 418;
    response.ContentType = MediaTypeNames.Text.Plain;
    //manual data in the response stream
    return response.WriteAsync("I am a Teapot!");
});
app.Run();
record Fruit(string Name, int Stock);