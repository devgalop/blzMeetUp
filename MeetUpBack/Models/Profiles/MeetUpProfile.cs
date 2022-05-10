using AutoMapper;
using MeetUpBack.Data.Entities;
using MeetUpBack.Models.Dto;

namespace MeetUpBack.Models.Profiles;

public class MeetUpProfile : Profile
{
    public MeetUpProfile()
    {
        CreateMap<AddMeetUpModel, MeetUp>();
        CreateMap<MeetUp, AddMeetUpModel>();

        CreateMap<UpdateMeetUpModel, MeetUp>();
        CreateMap<MeetUp, UpdateMeetUpModel>();
    }
}