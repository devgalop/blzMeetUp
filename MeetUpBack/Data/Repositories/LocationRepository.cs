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
        return await _dataContext.Locations
                .Include(l => l.MeetUps)
                .ToListAsync();
    }

    public async Task<List<Location>> GetLocationsByCity(int cityId)
    {
        return await _dataContext.Locations.Include(l => l.MeetUps).Where(c => c.CityId == cityId).ToListAsync();
    }

    public async Task<Location?> GetLocation(int id)
    {
        return await _dataContext.Locations.Include(l => l.MeetUps).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Location?> GetLocation(string name)
    {
        return await _dataContext.Locations.Include(l => l.MeetUps).Where(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task InsertCity(City city)
    {
        await _dataContext.Cities.AddAsync(city);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<City>> GetCities()
    {
        return await _dataContext.Cities
                .Include(c => c.Locations)
                .ToListAsync();
    }

    public async Task<City?> GetCity(int id)
    {
        return await _dataContext.Cities.Include(c => c.Locations).Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<City?> GetCity(string name)
    {
        return await _dataContext.Cities.Include(c => c.Locations).Where(c => c.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<City>> GetCitiesByCountry(int countryId)
    {
        return await _dataContext.Cities.Include(c => c.Locations).Where(c => c.CountryId == countryId).ToListAsync();
    }

    public async Task InsertCountry(Country country)
    {
        await _dataContext.Countries.AddAsync(country);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<Country>> GetCountries()
    {
        return await _dataContext.Countries
                .Include(c => c.Cities)
                .ToListAsync();
    }

    public async Task<Country?> GetCountry(int id)
    {
        return await _dataContext.Countries.Include(c => c.Cities).Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Country?> GetCountry(string name)
    {
        return await _dataContext.Countries.Include(c => c.Cities).Where(c => c.Name == name).FirstOrDefaultAsync();
    }
}