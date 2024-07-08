internal class EmailSender : IEmailSender
{
    public readonly MessageFactory _msgFactory;
    public readonly NetworkClient _networkClient;
    public EmailSender(MessageFactory msgFactory, NetworkClient networkClient)
    {
        _msgFactory = msgFactory;
        _networkClient = networkClient;
    }
    public void SendEmail(string user)
    {
        Email mail = _msgFactory.CreateEmail(user);
        _networkClient.SendMail(mail);
    }
}