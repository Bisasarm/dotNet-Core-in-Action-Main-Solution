using System.Collections.Concurrent;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//thread safe dictionary. uses more space and is performance heavier than a normal one
var _fruit = new ConcurrentDictionary<string, Fruit>();

//just the endpoint to get all of the fruit
app.MapGet("/fruit", () => _fruit);

//get a specific fruit and handle a not found with 404
app.MapGet("/fruit/{id}", (string id) => 
    _fruit.TryGetValue(id, out var fruit)
        ? TypedResults.Ok(fruit)
        : Results.NotFound());
//handle an already created fruit with a message and 400
app.MapPost("/fruit/{id}", (string id, Fruit fruit) =>
    _fruit.TryAdd(id,fruit)
        ? TypedResults.Created(id, fruit)
        : Results.BadRequest(new { id = "A fruit with this id already exists" })
);
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
//manual response generation
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