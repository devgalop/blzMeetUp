namespace MeetUpBack.Models.Dto;

public class BasicEventModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StartHour { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public InfoMeetUpModel MeetUp { get; set; } = null!;
}