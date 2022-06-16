using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Auth;

public class UpdateUserModel
{
    [Required(ErrorMessage = "Id is invalid")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name cannot be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name cannot be empty")]
    public string LastName { get; set; } = string.Empty;
    public bool Status { get; set; }
    public int RoleId { get; set; }
}