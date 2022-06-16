using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Location;

public class AddCityModel
{
    [Required(ErrorMessage = "City Name cannot be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contry Id is invalid")]
    public int CountryId { get; set; }
}