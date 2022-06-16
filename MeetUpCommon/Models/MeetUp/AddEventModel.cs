using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.MeetUp;

public class AddEventModel
{
    [Required(ErrorMessage = "Event Name cannot be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start Hour cannot be empty")]
    [RegularExpression(@"^([01][0-9]|2[0-3]):([0-5][0-9])$", ErrorMessage = "Invalid hour format. {HH:MM}")]
    public string StartHour { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start Hour cannot be empty")]
    public string Details { get; set; } = string.Empty;

    [Required(ErrorMessage = "Meet up Id is not valid")]
    public int MeetUpId { get; set; }
}