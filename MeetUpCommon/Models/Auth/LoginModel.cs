using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Auth;

public class LoginModel
{
    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; } = string.Empty;
}