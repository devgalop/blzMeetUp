namespace MeetUpBack.Models.Dto;

public class BasicMeetUpModel : InfoMeetUpModel
{
    public List<BasicEventModel> Events { get; set; } = null!;
}