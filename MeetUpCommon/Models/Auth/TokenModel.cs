namespace MeetUpCommon.Models.Auth;

public class TokenModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresIn { get; set; }
}