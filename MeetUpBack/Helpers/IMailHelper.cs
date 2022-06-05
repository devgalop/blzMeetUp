using MeetUpCommon.Models.Mail;

namespace MeetUpBack.Helpers;

public interface IMailHelper
{
    MailResponseModel Send(MailRequestModel model);
    Task<MailResponseModel> SendAsync(MailRequestModel model);
}