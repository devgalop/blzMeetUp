using AutoMapper;
using MeetUpBack.Data.Entities;
using MeetUpCommon.Models.MeetUp;

namespace MeetUpBack.Models.Profiles;

public class MeetUpProfile : Profile
{
    public MeetUpProfile()
    {
        CreateMap<AddMeetUpModel, MeetUp>();
        CreateMap<MeetUp, AddMeetUpModel>();
        CreateMap<BasicMeetUpModel, MeetUp>();
        CreateMap<MeetUp, BasicMeetUpModel>();
        CreateMap<InfoMeetUpModel, MeetUp>();
        CreateMap<MeetUp, InfoMeetUpModel>();
        CreateMap<UpdateMeetUpModel, MeetUp>();
        CreateMap<MeetUp, UpdateMeetUpModel>();

        CreateMap<AddEventModel, Event>();
        CreateMap<Event, AddEventModel>();
        CreateMap<UpdateEventModel, Event>();
        CreateMap<Event, UpdateEventModel>();
        CreateMap<BasicEventModel, Event>();
        CreateMap<Event, BasicEventModel>();
    }
}