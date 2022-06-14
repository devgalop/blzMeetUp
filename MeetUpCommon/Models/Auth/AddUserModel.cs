using System.ComponentModel.DataAnnotations;

namespace MeetUpCommon.Models.Auth;

public class AddUserModel
{
    [Required(ErrorMessage = "Name cannot be empty")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name cannot be empty")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email must be confirmed")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Compare("Email", ErrorMessage = "Email does not match")]
    public string ConfirmEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare("Password", ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    
    public int RoleId { get; set; }
}