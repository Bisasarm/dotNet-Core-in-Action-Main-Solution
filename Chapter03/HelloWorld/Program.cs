//No top level statements set
//this class creates a web server in form of a webapp and makes it listen to http requests
//this is a classic instance of the builder pattern - gang of four

//creates a WebApplication Builder using the createbuilder-Method provided by the class WebApplication
var builder = WebApplication.CreateBuilder(args);
//Using the builder to return an instance of the "WebApplication" class
var app = builder.Build();
//Defines the endpoint which, when called with the path "/", will result in "Hello World"
app.MapGet("/", () => "Hello World!");
//start the WebApp to listen for requests and generate responses
app.Run();
