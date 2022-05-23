using AutoMapper;
using MeetUpBack.Data.Entities;
using MeetUpBack.Models.Dto;

namespace MeetUpBack.Models.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<AddLocationModel, Location>();
        CreateMap<Location, AddLocationModel>();
        CreateMap<UpdateLocationModel, Location>();
        CreateMap<Location, UpdateLocationModel>();
        CreateMap<Location, BasicLocationModel>();
        CreateMap<BasicLocationModel, Location>();

        CreateMap<AddCityModel, City>();
        CreateMap<City, AddCityModel>();
        CreateMap<BasicCityModel, City>();
        CreateMap<City, BasicCityModel>();

        CreateMap<AddCountryModel, Country>();
        CreateMap<Country, AddCountryModel>();
        CreateMap<BasicCountryModel, Country>();
        CreateMap<Country, BasicCountryModel>();
    }
}