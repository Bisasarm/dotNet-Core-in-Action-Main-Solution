var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Exceptionhandling. Usually not necessary, since the app initializes this middleware by default
app.UseDeveloperExceptionPage();
//Errorhandlin in production is separate, with less details
if (app.Environment.IsProduction())
{
    //configured to invoke the path "/error", which was added as endpoint further below
    app.UseExceptionHandler("/error");
}

//Middleware to return static files. Is added to the middleware pipeline
app.UseStaticFiles();

//Middleware to enable endpoint routing. This is separate from the invisible EndpointMiddleware. It matches requests to routes. While the endpoint middleware looks for the endpoint and executes it.
app.UseRouting();

app.MapGet("/", () => "Hello World!");
app.MapGet("/error", () => "Sorry, an error occured");

app.Run();
