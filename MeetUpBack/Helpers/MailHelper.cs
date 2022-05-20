using MailKit.Net.Smtp;
using MailKit.Security;
using MeetUpBack.Models.Dto;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace MeetUpBack.Helpers;

public class MailHelper : IMailHelper
{
    private readonly SmtpConfigModel _config;

    public MailHelper(IOptions<SmtpConfigModel> config)
    {
        _config = config.Value;
    }

    public MailResponseModel Send(MailRequestModel model)
    {
        try
        {
            var email = GenerateMessage(model);
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_config.Host, _config.Port, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config.Username, _config.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
            return new MailResponseModel(){Status = true};
        }
        catch (Exception ex)
        {
            return new MailResponseModel()
            {
                Status = false,
                Message = ex.Message,
                Result = ex.ToString()
            };
        }
        
    }

    public async Task<MailResponseModel> SendAsync(MailRequestModel model)
    {
        try
        {
            var email = GenerateMessage(model);
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.SslOnConnect);
            await smtp.AuthenticateAsync(_config.Username, _config.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            return new MailResponseModel(){Status = true};
        }
        catch (Exception ex)
        {
            return new MailResponseModel()
            {
                Status = false,
                Message = ex.Message,
                Result = ex.ToString()
            };
        }
        
    }

    private MimeMessage GenerateMessage(MailRequestModel model)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(model.From ?? _config.EmailFrom));
        email.To.Add(MailboxAddress.Parse(model.To));
        email.Subject = model.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = model.Body };
        return email;
    }
}