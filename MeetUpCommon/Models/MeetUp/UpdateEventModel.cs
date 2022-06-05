namespace MeetUpCommon.Models.MeetUp;

public class UpdateEventModel : AddEventModel
{
    public int Id { get; set; }
    public bool Status { get; set; }
}