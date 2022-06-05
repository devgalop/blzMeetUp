using MeetUpCommon.Models.Location;

namespace MeetUpCommon.Models.MeetUp;

public class InfoMeetUpModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public bool Status { get; set; } = true;
    public BasicLocationModel Location { get; set; } = null!;
}