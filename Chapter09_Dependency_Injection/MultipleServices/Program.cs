var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMessager, EmailSender>();
builder.Services.AddScoped<IMessager, SmsSender>();
builder.Services.AddScoped<IMessager, FacebookSender>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/sendAll",(IEnumerable<IMessager> messagers ) =>
{
    foreach (var messager in messagers)
    {
        messager.SendMessage();
    }
});

app.MapGet("/sendOne", (IMessager messager) => messager.SendMessage());
app.Run();

public interface IMessager
{
    public void SendMessage();
}

public class EmailSender : IMessager
{
    public void SendMessage()
    {
        Console.WriteLine("Sending Mail");
    }
}
public class SmsSender : IMessager
{
    public void SendMessage()
    {
        Console.WriteLine("Sending SMS");
    }
}
public class FacebookSender : IMessager
{
    public void SendMessage()
    {
        Console.WriteLine("Sending Facebook Message");
    }
}