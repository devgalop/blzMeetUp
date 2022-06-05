namespace MeetUpCommon.Models.MeetUp;

public class BasicMeetUpModel : InfoMeetUpModel
{
    public List<BasicEventModel> Events { get; set; } = null!;
}