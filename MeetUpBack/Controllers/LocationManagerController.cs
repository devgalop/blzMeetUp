using MeetUpBack.Data.Entities;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MeetUpBack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationManagerController : ControllerBase
{
    private readonly ILocationRepository _repository;
    private readonly IMappingHelper _mappingHelper;

    public LocationManagerController(ILocationRepository repository,
                                    IMappingHelper mappingHelper)
    {
        _repository = repository;
        _mappingHelper = mappingHelper;
    }

    [HttpPost("RegisterCountry")]
    public async Task<IActionResult> RegisterCountry([FromBody] AddCountryModel model)
    {
        try
        {
            if (model == null || string.IsNullOrEmpty(model.Name)) throw new ArgumentNullException("Model is invalid");
            Country country = _mappingHelper.ConvertTo<Country, AddCountryModel>(model);
            await _repository.InsertCountry(country);
            var countryFound = await _repository.GetCountry(country.Name);
            if (countryFound == null) throw new Exception("Country has not been added to repository");
            return Ok(countryFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetCountries")]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            List<Country> countries = await _repository.GetCountries();
            List<BasicCountryModel> result = _mappingHelper.ConvertTo<List<BasicCountryModel>, List<Country>>(countries);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetCountry/{id}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        try
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id is invalid");
            var countryFound = await _repository.GetCountry(id);
            if (countryFound == null) return NotFound("Country has not been found");
            return Ok(countryFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPost("RegisterCity")]
    public async Task<IActionResult> RegisterCity([FromBody] AddCityModel model)
    {
        try
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || model.CountryId <= 0) throw new ArgumentNullException("Model is invalid");
            var countryFound = await _repository.GetCountry(model.CountryId);
            if (countryFound == null) throw new Exception("Country has not been found");
            City city = _mappingHelper.ConvertTo<City, AddCityModel>(model);
            await _repository.InsertCity(city);
            var cityFound = await _repository.GetCity(model.Name);
            if (cityFound == null) throw new Exception("City has not been added to repository");
            return Ok(cityFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetCities")]
    public async Task<IActionResult> GetCities()
    {
        try
        {
            List<City> cities = await _repository.GetCities();
            return Ok(cities);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetCity/{id}")]
    public async Task<IActionResult> GetCity(int id)
    {
        try
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id is invalid");
            var cityFound = await _repository.GetCity(id);
            if (cityFound == null) return NotFound("City has not been found");
            return Ok(cityFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPost("AddLocation")]
    public async Task<IActionResult> AddLocation([FromBody] AddLocationModel model)
    {
        try
        {
            if (model == null || model.CityId <= 0 || string.IsNullOrEmpty(model.Address) || model.Capacity <= 0) throw new ArgumentNullException("Model is invalid");
            City? cityFound = await _repository.GetCity(model.CityId);
            if (cityFound == null) throw new Exception("City has not been found");
            Location location = _mappingHelper.ConvertTo<Location, AddLocationModel>(model);
            await _repository.InsertLocation(location);
            var locationFound = await _repository.GetLocation(model.Name);
            if (locationFound == null) throw new Exception("Location has not been added to repository");
            return Ok(locationFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPut("UpdateLocation")]
    public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationModel model)
    {
        try
        {
            if (model == null || model.Id <= 0 || model.CityId <= 0 || string.IsNullOrEmpty(model.Address) || model.Capacity <= 0) throw new ArgumentNullException("Model is invalid");
            City? cityFound = await _repository.GetCity(model.CityId);
            if (cityFound == null) throw new Exception("City has not been found");
            Location? location = await _repository.GetLocation(model.Id);
            if(location == null) throw new Exception("Location has not been found");
            location.Name = model.Name;
            location.Address = model.Address;
            location.Capacity = model.Capacity;
            location.CityId = model.CityId;
            await _repository.UpdateLocation(location);
            return Ok(location);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpDelete("DeleteLocation/{id}")]
    public async Task<IActionResult> DeleteLocation(int id)
    {
        try
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id is invalid");
            Location? locationFound = await _repository.GetLocation(id);
            if(locationFound == null) throw new Exception("Location has not been found");
            await _repository.DeleteLocation(locationFound);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetLocations")]
    public async Task<IActionResult> GetLocations()
    {
        try
        {
            List<Location> locations = await _repository.GetLocations();
            return Ok(locations);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetLocation/{id}")]
    public async Task<IActionResult> GetLocation(int id)
    {
        try
        {
            if(id <= 0) throw new ArgumentOutOfRangeException("Id is out of range");
            Location? location = await _repository.GetLocation(id);
            if(location == null) return NotFound("Location has not been found");
            return Ok(location);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }
}