using MeetUpBack.Data.Entities;

namespace MeetUpBack.Data.Repositories;

public interface IAuthRepository
{
    Task RegisterUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
}