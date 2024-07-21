using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Razor_Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//This only works for generated links
builder.Services.Configure<RouteOptions>(o =>
{
    o.LowercaseQueryStrings = true;
    o.AppendTrailingSlash = true;
    o.LowercaseUrls = true;
});
//this also only works for generated links
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(o =>
    {
        o.Conventions.Add(new PageRouteTransformerConvention(new KebabCaseTransformator()));
        //this only works if mapped to the original path of the page, not if it gets replaced later. in this specific example it will be replaced by go, but the alternative route template is created beforehand
        o.Conventions.AddPageRoute("/testsite2", "/alternativeGo");
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
