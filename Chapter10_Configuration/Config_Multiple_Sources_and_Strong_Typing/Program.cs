using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();
//builder.Configuration.AddEnvironmentVariables();
//This will set the information level to Warning
builder.Configuration.AddJsonFile("appsettings.json");
//This should overwrite the information level to Debug
builder.Configuration.AddJsonFile("sharedsettings.json");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
var app = builder.Build();

app.MapGet("/", () => app.Configuration.AsEnumerable());
app.MapGet("/displaysettings", (IOptions<AppDisplaySettings> options) =>
{

});



app.Run();
internal class AppDisplaySettings
{
}