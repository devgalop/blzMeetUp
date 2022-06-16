using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Auth;

public class UpdatePasswordModel
{
    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare("Password", ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Password cannot be empty")]
    public string LastPassword { get; set; } = string.Empty;
}