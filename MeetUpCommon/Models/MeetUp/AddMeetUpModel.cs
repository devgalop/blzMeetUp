namespace MeetUpCommon.Models.MeetUp;

public class AddMeetUpModel
{
    public string Name { get; set; } = string.Empty;
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public int LocationId { get; set; }
}