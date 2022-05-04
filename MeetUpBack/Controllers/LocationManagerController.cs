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
            List<BasicCountryModel> result = _mappingHelper.ConvertTo<List<BasicCountryModel>,List<Country>>(countries);
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
            if (countryFound == null) throw new Exception("Country has not been found");
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
            if(model == null || string.IsNullOrEmpty(model.Name) || model.CountryId <= 0) throw new ArgumentNullException("Model is invalid");
            var countryFound = await _repository.GetCountry(model.CountryId);
            if(countryFound == null) throw new Exception("Country has not been found");
            City city = _mappingHelper.ConvertTo<City,AddCityModel>(model);
            await _repository.InsertCity(city);
            var cityFound = await _repository.GetCity(model.Name);
            if(cityFound == null) throw new Exception("City has not been added to repository");
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
            if (cityFound == null) throw new Exception("City has not been found");
            return Ok(cityFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

}