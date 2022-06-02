namespace MeetUpBack.Data.Entities;

public class UserMeetUpAssistant
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MeetUpId { get; set; }
    public DateTime ReservedAt { get; set; }
    public bool Status { get; set; } = true;
}