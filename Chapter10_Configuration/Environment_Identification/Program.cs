var builder = WebApplication.CreateBuilder(args);
//first the configuration sources are cleared for testing
builder.Configuration.Sources.Clear();
//the current environment instance is being written into a local variable/instance
IHostEnvironment env = builder.Environment;

//the configurationfiles are added
//note the appsettings.EnvironmentName is a dynamically generated name depending on the current environmentname
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
builder.Services.AddProblemDetails();

//Environments can be changed by changing the environment variables in the VS debug menu
//alternativeley the launchsettings.json has the environments set for different profiles
//launchsettings.json is usually only used for development scenarios and not for prod
if (env.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build();


if (!env.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.MapGet("/", () => "Hello World!");

app.Run();
