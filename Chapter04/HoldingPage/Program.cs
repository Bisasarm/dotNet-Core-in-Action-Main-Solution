var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Middleware added to the middleware pipeline
//the order in which the middleware is added is important, since it determines the flow through the pipeline
//The WelcomePage Middleware shortcircuits the pipeline and immedeiately creates an html response in form of the WelcomePage, no matter the called path
app.UseWelcomePage();

app.Run();
