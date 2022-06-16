using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.MeetUp;

public class UpdateEventModel : AddEventModel
{
    [Required(ErrorMessage = "Event Id is invalid")]
    public int Id { get; set; }
    public bool Status { get; set; }
}