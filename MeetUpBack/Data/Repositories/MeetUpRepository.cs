using MeetUpBack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpBack.Data.Repositories;

public class MeetUpRepository : IMeetUpRepository
{
    private readonly DataContext _dataContext;

    public MeetUpRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<MeetUp>> GetMeetUps()
    {
        return await _dataContext.MeetUps.ToListAsync();
    }

    public async Task CreateMeetUp(MeetUp meetUp)
    {
        _dataContext.MeetUps.Add(meetUp);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateMeetUp(MeetUp meetUp)
    {
        _dataContext.MeetUps.Update(meetUp);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteMeetUp(MeetUp meetUp)
    {
        meetUp.Status = false;
        _dataContext.MeetUps.Update(meetUp);
        await _dataContext.SaveChangesAsync();
    }

}