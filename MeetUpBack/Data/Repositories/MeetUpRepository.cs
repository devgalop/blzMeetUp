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

    public async Task<List<MeetUp>> GetMeetUpsByLocation(int locationId)
    {
        return await _dataContext.MeetUps.Where(m => m.LocationId == locationId).ToListAsync();
    }

    public async Task<MeetUp?> GetMeetUp(int id)
    {
        return await _dataContext.MeetUps.Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<MeetUp?> GetMeetUp(string name)
    {
        return await _dataContext.MeetUps.Where(m => m.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<MeetUp>> GetMeetUpsByDate(DateTime date)
    {
        return await _dataContext.MeetUps.Where(m => m.InitialDate == date).ToListAsync();
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
        await UpdateMeetUp(meetUp);
    }

    public async Task CreateEvent(Event meetUpEvent)
    {
        _dataContext.Events.Add(meetUpEvent);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateEvent(Event meetUpEvent)
    {
        _dataContext.Events.Update(meetUpEvent);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteEvent(Event meetUpEvent)
    {
        meetUpEvent.Status = false;
        await UpdateEvent(meetUpEvent);
    }

}