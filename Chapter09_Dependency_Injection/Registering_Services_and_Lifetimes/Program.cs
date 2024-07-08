var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<NetworkClient>();
builder.Services.AddSingleton<MessageFactory>();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/register/{user}", (RegisterUser));

app.Run();

string RegisterUser(string userName, IEmailSender emailSender)
{
    emailSender.SendEmail(userName);
    return $"Email sent to {userName}";
}
