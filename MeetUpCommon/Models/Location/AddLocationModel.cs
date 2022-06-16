using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Location;

public class AddLocationModel
{
    [Required(ErrorMessage = "Location Name cannot be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address cannot be empty")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Capacity is invalid")]
    [Range(1, Int32.MaxValue)]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "City Id is invalid")]
    public int CityId { get; set; }
}