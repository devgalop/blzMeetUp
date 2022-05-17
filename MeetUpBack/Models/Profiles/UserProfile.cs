using AutoMapper;
using MeetUpBack.Data.Entities;
using MeetUpBack.Models.Dto;

namespace MeetUpBack.Models.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserModel, User>();
        CreateMap<User,AddUserModel>();
        CreateMap<UpdateUserModel, User>();
        CreateMap<User,UpdateUserModel>();
        CreateMap<BasicUserModel, User>();
        CreateMap<User, BasicUserModel>();
    }
}