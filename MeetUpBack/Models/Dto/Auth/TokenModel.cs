namespace MeetUpBack.Models.Dto;

public class TokenModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresIn { get; set; }
}