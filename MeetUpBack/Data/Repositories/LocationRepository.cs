using MeetUpBack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetUpBack.Data.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DataContext _dataContext;

    public LocationRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task InsertLocation(Location location)
    {
        await _dataContext.Locations.AddAsync(location);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateLocation(Location location)
    {
        _dataContext.Locations.Update(location);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteLocation(Location location)
    {
        _dataContext.Locations.Remove(location);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Location>> GetLocations()
    {
        return await _dataContext.Locations.ToListAsync();
    }

    public async Task<List<Location>> GetLocationsByCity(int cityId)
    {
        return await _dataContext.Locations.Where(c => c.CityId == cityId).ToListAsync();
    }

    public async Task<Location?> GetLocation(int id)
    {
        return await _dataContext.Locations.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task InsertCity(City city)
    {
        await _dataContext.Cities.AddAsync(city);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<City>> GetCities()
    {
        return await _dataContext.Cities.ToListAsync();
    }

    public async Task<City?> GetCity(int id)
    {
        return await _dataContext.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<City>> GetCitiesByCountry(int countryId)
    {
        return await _dataContext.Cities.Where(c => c.CountryId == countryId).ToListAsync();
    }

    public async Task InsertCountry(Country country)
    {
        await _dataContext.Countries.AddAsync(country);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Country>> GetCountries()
    {
        return await _dataContext.Countries.ToListAsync();
    }

    public async Task<Country?> GetCountry(int id)
    {
        return await _dataContext.Countries.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
}