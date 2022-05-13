using BC = BCrypt.Net.BCrypt;
namespace MeetUpBack.Helpers;

public class PasswordManagerHelper : IPasswordManagerHelper
{
    public string GenerateHashCode(string stringToHash)
        {
            return stringToHash.Length > 0 ?
                BC.HashPassword(stringToHash) :
                "";
        }

        public bool IsValidHashCode(string stringToVerify, string hashedString)
        {
            return (stringToVerify.Length > 0 && hashedString.Length > 0) ?
                BC.Verify(stringToVerify, hashedString) :
                false;
        }
}