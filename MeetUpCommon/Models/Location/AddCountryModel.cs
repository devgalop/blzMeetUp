using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Location;

public class AddCountryModel
{
    [Required(ErrorMessage = "Country Name cannot be empty")]
    public string Name { get; set; } = string.Empty;
}