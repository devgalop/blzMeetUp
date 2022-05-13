using MeetUpBack.Data.Entities;
using MeetUpBack.Models.Dto;

namespace MeetUpBack.Helpers;

public interface ITokenFactoryHelper
{
    TokenModel GenerateToken(User user);
}