using MeetUpBack.Data.Entities;

namespace MeetUpBack.Data.Repositories;

public interface IMeetUpRepository
{
    Task<List<MeetUp>> GetMeetUps();
    Task CreateMeetUp(MeetUp meetUp);
    Task UpdateMeetUp(MeetUp meetUp);
    Task DeleteMeetUp(MeetUp meetUp);
}