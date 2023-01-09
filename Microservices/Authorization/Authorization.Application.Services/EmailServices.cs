using System.Net;
using System.Net.Mail;
using System.Web;
using Authorization.Application.Helpers;
using Authorization.Application.Resources;
using Authorization.Data_Domain.Models;
using Microsoft.Extensions.Options;

namespace Authorization.Application.Services;

public interface IEmailServices
{
    public Task SendConfirmEmailAsync(Account userId, string subject, string token,
        CancellationToken cancellationToken);
}

public class EmailServices : IEmailServices
{
    private readonly EmailSettings _emailSettings;

    public EmailServices(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendConfirmEmailAsync(Account user, string subject, string token,
        CancellationToken cancellationToken)
    {
        var encodedToken = HttpUtility.UrlEncode(token);
        var callbackUrl = $"https://localhost:44336/api/User/confirmation/{user.Id}?token={encodedToken}";
        var content = string.Format(Messages.HtmlMessage,
            string.Join(" ", user.LastName, user.FirstName, user.MiddleName), callbackUrl);

        var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
            EnableSsl = true
        };

        var from = new MailAddress(_emailSettings.Email, Messages.DisplayName);
        var to = new MailAddress(user.Email);
        using var mailMessage = new MailMessage(from, to)
        {
            Subject = subject,
            Body = content,
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
    }
}