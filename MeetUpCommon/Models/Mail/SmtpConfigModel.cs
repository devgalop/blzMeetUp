namespace MeetUpCommon.Models.Mail;

public class SmtpConfigModel
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string EmailFrom { get; set; } = string.Empty;
}