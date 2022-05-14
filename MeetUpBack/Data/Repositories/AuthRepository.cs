using MeetUpBack.Data.Entities;

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
}