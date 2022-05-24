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
        return await _dataContext.MeetUps
                .Include(m => m.Location)
                .Where(m => m.Status)
                .ToListAsync();
    }

    public async Task<List<MeetUp>> GetMeetUpsByLocation(int locationId)
    {
        return await _dataContext.MeetUps
                .Include(m => m.Location)
                .Where(m => m.LocationId == locationId && m.Status)
                .ToListAsync();
    }

    public async Task<MeetUp?> GetMeetUp(int id)
    {
        return await _dataContext.MeetUps
                .Include(m => m.Location)
                .Where(m => m.Id == id && m.Status)
                .FirstOrDefaultAsync();
    }

    public async Task<MeetUp?> GetMeetUp(string name)
    {
        return await _dataContext.MeetUps
                .Include(m => m.Location)
                .Where(m => m.Name == name && m.Status)
                .FirstOrDefaultAsync();
    }

    public async Task<List<MeetUp>> GetMeetUpsByDate(DateTime date)
    {
        return await _dataContext.MeetUps
                .Include(m => m.Location)
                .Where(m => m.InitialDate == date && m.Status)
                .ToListAsync();
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

    public async Task<Event?> GetEvent(int id)
    {
        return await _dataContext.Events
                .Include(e => e.MeetUp)
                .Where(x => x.Id == id && x.Status)
                .FirstOrDefaultAsync();
    }

    public async Task<Event?> GetEvent(string name)
    {
        return await _dataContext.Events
                .Include(e => e.MeetUp)
                .Where(x => x.Name == name && x.Status)
                .FirstOrDefaultAsync();
    }

    public async Task<List<Event>> GetEventsByMeetUp(int meetUpId)
    {
        return await _dataContext.Events
                .Include(e => e.MeetUp)
                .Where(x => x.MeetUpId == meetUpId && x.Status)
                .ToListAsync();
    }

    public async Task AssignMeetUpOwner(UserMeetUpOwner owner)
    {
        _dataContext.Owners.Add(owner);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeassignMeetUpOwner(UserMeetUpOwner owner)
    {
        _dataContext.Owners.Remove(owner);
        await _dataContext.SaveChangesAsync();
    }

    public async Task RegisterAttendance(UserMeetUpAssistant user)
    {
        _dataContext.Attendees.Add(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task RemoveAttendance(UserMeetUpAssistant user)
    {
        _dataContext.Attendees.Remove(user);
        await _dataContext.SaveChangesAsync();
    }

}