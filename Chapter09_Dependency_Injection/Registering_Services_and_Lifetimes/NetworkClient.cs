internal class NetworkClient
{
    public readonly EmailServerSettings _emailServerSettings;
    public NetworkClient(EmailServerSettings emailServerSettings)
    {
        _emailServerSettings = emailServerSettings;
    }
    public void SendMail(Email email)
    {
        Console.WriteLine($"Connecting to: {_emailServerSettings.Host} Port: {_emailServerSettings.Port}");
        Console.WriteLine($"Mail sent to {email.User}. Message: {email.Message}");
    }
}