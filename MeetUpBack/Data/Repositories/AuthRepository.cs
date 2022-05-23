using MeetUpBack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpBack.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _dataContext;

    public AuthRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task RegisterUser(User user)
    {
        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        _dataContext.Users.Update(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        user.Status = false;
        await UpdateUser(user);
    }

    public async Task<User?> GetUser(string email)
    {
        return await _dataContext.Users
                                    .Include(s => s.Session)
                                    .Include(r => r.Role)
                                    .Where(u => u.Email == email && u.Status)
                                    .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        return await _dataContext.Users
                                    .Include(s => s.Session)
                                    .Include(r => r.Role)
                                    .Where(u => u.Id == id && u.Status)
                                    .FirstOrDefaultAsync();
    }

    public async Task AddRole(Role role)
    {
        _dataContext.Roles.Add(role);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateRole(Role role)
    {
        _dataContext.Roles.Update(role);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteRole(Role role)
    {
        role.Status = false;
        await UpdateRole(role);
    }

    public async Task<List<Role>> GetRoles()
    {
        return await _dataContext.Roles.Where(r => r.Status).ToListAsync();
    }

    public async Task<Role?> GetRole(int id)
    {
        return await _dataContext.Roles
                                    .Where(r => r.Id == id && r.Status)
                                    .FirstOrDefaultAsync();
    }

    public async Task<Role?> GetRole(string name)
    {
        return await _dataContext.Roles
                                    .Where(r => r.Name == name && r.Status)
                                    .FirstOrDefaultAsync();
    }

    public async Task InsertSession(Session session)
    {
        _dataContext.Sessions.Add(session);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateSession(Session session)
    {
        _dataContext.Sessions.Update(session);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteSession(Session session)
    {
        _dataContext.Sessions.Remove(session);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Session?> GetSession(int userId)
    {
        return await _dataContext.Sessions
                                    .Include(s => s.User)
                                    .Where(s => s.UserId == userId)
                                    .FirstOrDefaultAsync();
    }
}