using MeetUpBack.Data.Entities;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MeetUpBack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetUpManagerController : ControllerBase
{
    private readonly IMeetUpRepository _repository;
    private readonly ILocationRepository _locationRepo;
    private readonly IMappingHelper _mappingHelper;

    public MeetUpManagerController(IMeetUpRepository repository,
                                    ILocationRepository locationRepo,
                                    IMappingHelper mappingHelper)
    {
        _repository = repository;
        _locationRepo = locationRepo;
        _mappingHelper = mappingHelper;
    }

    [HttpPost("CreateMeetUp")]
    public async Task<IActionResult> CreateMeetUp([FromBody] AddMeetUpModel model)
    {
        try
        {
            if(model == null || string.IsNullOrEmpty(model.Name) || model.LocationId <= 0) throw new ArgumentNullException("Model is invalid");
            if(model.FinalDate < DateTime.Now || model.FinalDate < model.InitialDate) throw new Exception("Final date is not valid");
            if(model.InitialDate < DateTime.Now) throw new Exception("Initial date is not valid");
            MeetUp meetUp = _mappingHelper.ConvertTo<MeetUp,AddMeetUpModel>(model);
            await _repository.CreateMeetUp(meetUp);
            var meetUpFound = await _repository.GetMeetUp(meetUp.Name);
            if(meetUpFound == null)throw new Exception("Meet Up has not been added to repository");
            return Ok(meetUpFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPut("UpdateMeetUp")]
    public async Task<IActionResult> UpdateMeetUp([FromBody] UpdateMeetUpModel model)
    {
        try
        {
            if(model == null || model.Id <= 0 || string.IsNullOrEmpty(model.Name) || model.LocationId <= 0) throw new ArgumentNullException("Model is invalid");
            if(model.FinalDate < DateTime.Now || model.FinalDate < model.InitialDate) throw new Exception("Final date is not valid");
            if(model.InitialDate < DateTime.Now) throw new Exception("Initial date is not valid");
            MeetUp? meetUpFound = await _repository.GetMeetUp(model.Id);
            if(meetUpFound == null) return NotFound("Meet Up has not been found");
            meetUpFound.Name = model.Name;
            meetUpFound.InitialDate = model.InitialDate;
            meetUpFound.FinalDate = model.FinalDate;
            meetUpFound.LocationId = model.LocationId;
            await _repository.UpdateMeetUp(meetUpFound);
            return Ok(meetUpFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpDelete("DeleteMeetUp/{id}")]
    public async Task<IActionResult> DeleteMeetUp(int id)
    {
        try
        {
            if(id <= 0) throw new ArgumentOutOfRangeException("Id is out of range");
            MeetUp? meetUpFound = await _repository.GetMeetUp(id);
            if(meetUpFound == null) return NotFound("Meet up has not been found");
            await _repository.DeleteMeetUp(meetUpFound);
            return Ok("Meet up has been deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetAllMeetUps")]
    public async Task<IActionResult> GetAllMeetUps()
    {
        try
        {
            List<MeetUp> meetUpList = await _repository.GetMeetUps();
            return Ok(meetUpList);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetMeetUpByLocation/{locationId}")]
    public async Task<IActionResult> GetMeetUpByLocation(int locationId)
    {
        try
        {
            if(locationId <= 0)throw new ArgumentOutOfRangeException("Location is not valid");
            Location? locationFound = await _locationRepo.GetLocation(locationId);
            if(locationFound == null)throw new Exception("Location does not exist");
            List<MeetUp> meetUpList = await _repository.GetMeetUpsByLocation(locationId);
            return Ok(meetUpList);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    
}