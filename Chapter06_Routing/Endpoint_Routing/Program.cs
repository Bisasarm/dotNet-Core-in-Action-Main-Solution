var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddRazorPages();
var app = builder.Build();

app.MapGet("/test", () => "Hello World!");
app.MapHealthChecks("/healthz");

//Register ALL the razor pages in the application as endpoints
app.MapRazorPages();
app.Run();
