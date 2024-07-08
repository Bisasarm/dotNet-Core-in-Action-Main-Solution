internal class MessageFactory
{
    public Email CreateEmail(string user)
        => new Email(user, $"Mail sent to {user}");    
}