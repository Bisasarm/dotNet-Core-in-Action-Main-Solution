//No top level statements set
//this class creates a web server in form of a webapp and makes it listen to http requests
//this is a classic instance of the builder pattern - gang of four

//´getting the HTTPLogging Service
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;

//creates a WebApplication Builder using the createbuilder-Method provided by the class WebApplication
var builder = WebApplication.CreateBuilder(args);
// adds the extension method "addhttplogging" to the builder and configures it by adding the relevant options. In this case the enum loggingfields
builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);
//Adding the "filter" to the logging. In this case information
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);
//Using the builder to return an instance of the "WebApplication" class
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //activates the logging middleware
    app.UseHttpLogging();
}
//Defines the endpoint which, when called with the path "/", will result in "Hello World"
app.MapGet("/", () => "Hello World!");
//define an endpoint for the path "/person"
//note: http responses in ASP.NET Core by default return data as serizalized json
app.MapGet("/person", () => new Person("Luis", "Sarmiento", "test"));
//start the WebApp to listen for requests and generate responses
app.Run();
//record creation to automatically enable the creation of immutable instances of an object "person" with the properties as defined
public record Person(string FirstName, string LastName, string testToExtend);
