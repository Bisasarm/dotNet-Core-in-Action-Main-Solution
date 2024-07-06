using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

//this can be overriden by LinkOptions
builder.Services.Configure<RouteOptions>(o =>
{
    o.LowercaseQueryStrings = false;
    o.AppendTrailingSlash = true;
    o.LowercaseUrls = true;
});

var app = builder.Build();

//upper case routing path to test the routeoptions during the redirect
app.MapGet("/TEST", () => "Hello World!").WithName("hello");

const string PRODUCT_NAME = "product";
//giving the endpoint product/name a metadata name to use it for link generating
app.MapGet("/product/{name}", (string name) => $"the product name is{name}").WithName(PRODUCT_NAME);

//linkgenerating
app.MapGet("/links", (LinkGenerator linkGen) =>
    {
        return linkGen.GetPathByName(PRODUCT_NAME, new{ name = "big-widget" });
    }
);

//linkgenerating with a full path
//the parameters are given via an anonymous C# object
app.MapGet("/fullpath", (LinkGenerator linkGen) =>
{
    return linkGen.GetUriByName(PRODUCT_NAME, new { name = "fullpathwidget" }, "https", new HostString("localhost"));
}
);
//throws an exception because the param is not defined
app.MapGet("/wrongParams", (LinkGenerator linkGen) =>
{
    return linkGen.GetUriByName(PRODUCT_NAME, new { name = "testProduct", wrongParam = "ALLUPERCASE" }, "https", new HostString("localhost"));
}
);
//just redirects using the global routingoptions
app.MapGet("/redirect", () =>
    Results.RedirectToRoute("hello")
    );

//just redirects to hello world with specified link options
app.MapGet("/routingOptions", (LinkGenerator links) =>
    {
        string path = links.GetPathByName("hello", values: null, options: new LinkOptions
        {
            LowercaseUrls = false,
            AppendTrailingSlash = false
        });
        return Results.Redirect(path);
    });

//app.MapGet("/redirectGoogle", () => Results.Redirect("www.google.de"));

app.Run();
