using MeetUpBack.Data.Entities;

namespace MeetUpBack.Data.Repositories;

public interface ILocationRepository
{
    Task InsertCountry(Country country);
    Task<List<Country>> GetCountries();
    Task<Country?> GetCountry(int id);
    Task<Country?> GetCountry(string name);
    Task InsertCity(City city);
    Task<List<City>> GetCities();
    Task<City?> GetCity(int id);
    Task<City?> GetCity(string name);
    Task<List<City>> GetCitiesByCountry(int countryId);
    Task InsertLocation(Location location);
    Task UpdateLocation(Location location);
    Task DeleteLocation(Location location);
    Task<List<Location>> GetLocations();
    Task<List<Location>> GetLocationsByCity(int cityId);
    Task<Location?> GetLocation(int id);
    Task<Location?> GetLocation(string name);
}