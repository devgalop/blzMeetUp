namespace MeetUpCommon.Models.Mail;

public class MailResponseModel
{
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
}