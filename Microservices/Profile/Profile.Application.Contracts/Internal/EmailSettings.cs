namespace Profile.Application.Contracts.Internal;

public class EmailSettings
{
    public string Email { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}