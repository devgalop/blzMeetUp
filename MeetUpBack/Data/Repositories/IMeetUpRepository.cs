using MeetUpBack.Data.Entities;

namespace MeetUpBack.Data.Repositories;

public interface IMeetUpRepository
{
    Task<List<MeetUp>> GetMeetUps();
    Task CreateMeetUp(MeetUp meetUp);
    Task UpdateMeetUp(MeetUp meetUp);
    Task DeleteMeetUp(MeetUp meetUp);
    Task<List<MeetUp>> GetMeetUpsByLocation(int locationId);
    Task<MeetUp?> GetMeetUp(int id);
    Task<MeetUp?> GetMeetUp(string name);
    Task<List<MeetUp>> GetMeetUpsByDate(DateTime date);
    Task CreateEvent(Event meetUpEvent);
    Task UpdateEvent(Event meetUpEvent);
    Task DeleteEvent(Event meetUpEvent);
    Task<Event?> GetEvent(int id);
    Task<Event?> GetEvent(string name);
    Task<List<Event>> GetEventsByMeetUp(int meetUpId);
    Task AssignMeetUpOwner(UserMeetUpOwner owner);
    Task DeassignMeetUpOwner(UserMeetUpOwner owner);
    Task RegisterAttendance(UserMeetUpAssistant user);
    Task RemoveAttendance(UserMeetUpAssistant user);
}