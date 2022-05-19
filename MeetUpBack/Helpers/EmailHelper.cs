
using MailKit.Net.Smtp;
using MailKit.Security;
using MeetUpBack.Models.Dto;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace MeetUpBack.Helpers;

public class EmailHelper : IEmailHelper
{
    private readonly SmtpConfigModel _config;

    public EmailHelper(IOptions<SmtpConfigModel> config)
    {
        _config = config.Value;
    }

    public void Send(string to, string subject, string html, string from = null!)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _config.EmailFrom));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.Host, _config.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.Username, _config.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public async Task SendAsync(string to, string subject, string html, string from = null!)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _config.EmailFrom));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.Host, _config.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.Username, _config.Password);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}