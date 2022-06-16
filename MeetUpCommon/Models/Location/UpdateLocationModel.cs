using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Location;

public class UpdateLocationModel : AddLocationModel
{
    [Required(ErrorMessage = "Id is invalid")]
    public int Id { get; set; }
}