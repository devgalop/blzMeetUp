using MeetUpBack.Data.Entities;
using MeetUpCommon.Models.Auth;

namespace MeetUpBack.Helpers;

public interface ITokenFactoryHelper
{
    TokenModel GenerateToken(User user);
}