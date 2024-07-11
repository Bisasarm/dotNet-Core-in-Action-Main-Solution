var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
var app = builder.Build();

//Retreiving all of the Config as Enumerable
app.MapGet("/", () => app.Configuration.AsEnumerable());
//Accessing a specific key value pair
app.MapGet("/logginglevel", () => app.Configuration["Logging:LogLevel:Microsoft.AspNetCore"]);
app.Run();
