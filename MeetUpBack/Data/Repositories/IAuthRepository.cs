using MeetUpBack.Data.Entities;

namespace MeetUpBack.Data.Repositories;

public interface IAuthRepository
{
    Task RegisterUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
    Task<User?> GetUser(string email);
    Task<User?> GetUser(int id);
    Task AddRole(Role role);
    Task UpdateRole(Role role);
    Task DeleteRole(Role role);
    Task<Role?> GetRole(int id);
    Task<Role?> GetRole(string name);
    Task InsertSession(Session session);
    Task UpdateSession(Session session);
    Task DeleteSession(Session session);
    Task<Session?> GetSession(int userId);
}