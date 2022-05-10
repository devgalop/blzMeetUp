using System.ComponentModel.DataAnnotations;

namespace MeetUpBack.Models.Dto;

public class AddEventModel
{
    public string Name { get; set; } = string.Empty;
    [RegularExpression(@"^([01][0-9]|2[0-3]):([0-5][0-9])$")]
    public string StartHour { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int MeetUpId { get; set; }
}