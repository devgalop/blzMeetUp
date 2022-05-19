namespace MeetUpBack.Helpers;

public interface IEmailHelper
{
    void Send(string to, string subject, string html, string from = null!);
    Task SendAsync(string to, string subject, string html, string from = null!);
}