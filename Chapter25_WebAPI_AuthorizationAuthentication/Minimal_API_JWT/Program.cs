using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
//adding authentication service
//adding jwt bearer service
builder.Services.AddAuthentication().AddJwtBearer();
//adding authorization service
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
//Adding authorized only endpoint
app.MapGet("/jwt", [Authorize]() => "Hello JWT!");

app.Run();
