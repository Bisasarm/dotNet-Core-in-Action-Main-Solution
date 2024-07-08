var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IEmailSender, EmailSender>();
//builder.Services.AddScoped<NetworkClient>();
//builder.Services.AddSingleton<MessageFactory>();
//this is the endgoal. to have a collection of all the necessary services and add them at once for the DI container
//adding the servicecollection to the services makes sure that the Objects are being created when necessary by the ASP.NET DI Container
builder.Services.AddEmailSender();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

//Endpoint which invokes the register user and uses the IEmailsender
//only uses IEmailSender if it was built and bound via DI container
app.MapGet("/register/{user}", (RegisterUser));

app.Run();

//handler method
string RegisterUser(string user, IEmailSender emailSender)
{
    emailSender.SendEmail(user);
    return $"Email sent to {user}";
}
//ServiceCollection which extends via IServiceCollection
public static class EmailSenderServiceCollectionExtensions
{
    /// <summary>
    /// collect necessary services for emailsender service
    /// </summary>
    /// <param name="services">the most important part. "this" is used to extend the IServiceCollection</param>
    /// <returns>returns all services added to an implementation of IServiceCollection</returns>
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        //the whole dependency graph is being collected and registered here
        //EmailSender depends on MessageFactory and NetworkClient
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<MessageFactory>();
        //Networkclient depends on EmailServersettings
        services.AddSingleton<NetworkClient>();
        services.AddSingleton(new EmailServerSettings(
            Host: "smtp.server.com",
            Port: 25
            ));
        return services;
    }
}
//Mail record
public record Email(string User, string Message);
//ServerSettings record. Is initialized once due to singleton lifetime
public record EmailServerSettings(string Host, int Port);

