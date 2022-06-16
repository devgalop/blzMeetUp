using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.MeetUp;

public class AddMeetUpModel
{
    [Required(ErrorMessage = "Name cannot be empty")]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Date, ErrorMessage ="The field Initial Date must be a Date")]
    public DateTime InitialDate { get; set; }

    [DataType(DataType.Date, ErrorMessage ="The field Final Date must be a Date")]
    public DateTime FinalDate { get; set; }

    [Required(ErrorMessage = "Location Id is invalid")]
    public int LocationId { get; set; }
}