using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.MeetUp;

public class UpdateMeetUpModel : AddMeetUpModel
{
    [Required(ErrorMessage = "Meet Up Id is invalid")]
    public int Id { get; set; }
}