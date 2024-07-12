var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();
//builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("sharedsettings.json");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
var app = builder.Build();

app.MapGet("/", () => app.Configuration.AsEnumerable());

app.Run();
