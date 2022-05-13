namespace MeetUpBack.Helpers;

public interface IPasswordManagerHelper
{
    string GenerateHashCode(string stringToHash);
    bool IsValidHashCode(string stringToVerify, string hashedString);
}