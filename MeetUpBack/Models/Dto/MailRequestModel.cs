namespace MeetUpBack.Models.Dto;

public class MailRequestModel
{
    public string From { get; set; } = null!;
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}