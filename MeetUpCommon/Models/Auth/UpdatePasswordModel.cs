namespace MeetUpCommon.Models.Auth;

public class UpdatePasswordModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string LastPassword { get; set; } = string.Empty;
}