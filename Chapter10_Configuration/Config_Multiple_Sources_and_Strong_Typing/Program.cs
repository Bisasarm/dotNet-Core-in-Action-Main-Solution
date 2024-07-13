using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();
//builder.Configuration.AddEnvironmentVariables();
//This will set the information level to Warning
builder.Configuration.AddJsonFile("appsettings.json");
//This should overwrite the information level to Debug
builder.Configuration.AddJsonFile("sharedsettings.json");
//injecting the Service for binded Configuration
builder.Services.Configure<AppDisplaySettings>(builder.Configuration.GetSection(nameof(AppDisplaySettings)));

//builder.Services.Configure<MapSettings>(builder.Configuration.GetSection(nameof(MapSettings)));
//binding configuration to POCO without IOptions
var mapsettings = new MapSettings();
builder.Configuration.GetSection(nameof(MapSettings)).Bind(mapsettings);
builder.Services.AddSingleton(mapsettings);
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
var app = builder.Build();

app.MapGet("/", () => app.Configuration.AsEnumerable());
app.MapGet("/displaysettings", (IOptionsSnapshot<AppDisplaySettings> options) =>
{
    AppDisplaySettings settings = options.Value;
    string title = settings.Title;
    bool copyright = settings.ShowCopyright;
    return new { title, copyright };
});
app.MapGet("/Mapsettings", () => mapsettings.DefaultLocation);


app.Run();
internal class AppDisplaySettings
{
    public string Title { get; set; }
    public bool ShowCopyright { get; set; }

}
internal class MapSettings
{
    public int DefaultZoomlevel { get; set; }
    public DefaultLocation DefaultLocation { get; set; }
}

public class DefaultLocation
{
    public int Latitude { get; set; }
    public int Longitude { get; set; }
}