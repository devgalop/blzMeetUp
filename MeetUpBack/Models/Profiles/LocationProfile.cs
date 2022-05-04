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

        CreateMap<AddCityModel, City>();
        CreateMap<City, AddCityModel>();
        CreateMap<UpdateCityModel, City>();
        CreateMap<City, UpdateCityModel>();

        CreateMap<AddCountryModel, Country>();
        CreateMap<Country, AddCountryModel>();
        CreateMap<UpdateCountryModel, Country>();
        CreateMap<Country, UpdateCountryModel>();
    }
}