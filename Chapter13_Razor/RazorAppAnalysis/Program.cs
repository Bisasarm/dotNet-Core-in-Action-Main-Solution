var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//this middleware ensures that the app only responds to https requests
app.UseHttpsRedirection();
//this middleware would be added anyways, but by explicitly adding it, it can be determined WHERE the middleware is in the pipline
//this way the middleware pipeleine could be short circuited at a self designated point
app.UseStaticFiles();
//middleware that enables routing specifically AFTER static files
app.UseRouting();
//adds authorization functionality, but must be configured to be used
app.UseAuthorization();
//middleware that maps all the razor pages in /pages as an endpoint
app.MapRazorPages();

app.Run();
