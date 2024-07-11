var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
var app = builder.Build();

app.MapGet("/", () => app.Configuration.AsEnumerable());

app.Run();
