using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Profile.Application.Contracts.Internal;

namespace Profile.Application.Services;

public interface IEmailServices
{
    public Task SendEmailAsync(string recipientEmail, string subject, string content,
        CancellationToken cancellationToken);
}

public class EmailServices : IEmailServices
{
    private readonly EmailSettings _emailSettings;

    public EmailServices(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string content,
        CancellationToken cancellationToken)
    {
        var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
            EnableSsl = true
        };

        var from = new MailAddress(_emailSettings.Email, "Hospital");
        var to = new MailAddress(recipientEmail);
        using var mailMessage = new MailMessage(from, to)
        {
            Subject = subject,
            Body = content
        };

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
    }
}