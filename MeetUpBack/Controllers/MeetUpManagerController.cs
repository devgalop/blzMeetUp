using System.Text.RegularExpressions;
using MeetUpBack.Data.Entities;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetUpBack.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MeetUpManagerController : ControllerBase
{
    private readonly IMeetUpRepository _repository;
    private readonly ILocationRepository _locationRepo;
    private readonly IAuthRepository _userRepository;
    private readonly IMappingHelper _mappingHelper;

    public MeetUpManagerController(IMeetUpRepository repository,
                                    ILocationRepository locationRepo,
                                    IAuthRepository userRepository,
                                    IMappingHelper mappingHelper)
    {
        _repository = repository;
        _locationRepo = locationRepo;
        _userRepository = userRepository;
        _mappingHelper = mappingHelper;
    }

    [HttpPost("CreateMeetUp")]
    public async Task<IActionResult> CreateMeetUp([FromBody] AddMeetUpModel model)
    {
        try
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || model.LocationId <= 0) throw new ArgumentNullException("Model is invalid");
            if (model.FinalDate < DateTime.Now || model.FinalDate < model.InitialDate) throw new Exception("Final date is not valid");
            if (model.InitialDate < DateTime.Now) throw new Exception("Initial date is not valid");
            Location? locationFound = await _locationRepo.GetLocation(model.LocationId);
            if (locationFound == null) throw new Exception("Location does not exist");
            MeetUp meetUp = _mappingHelper.ConvertTo<MeetUp, AddMeetUpModel>(model);
            await _repository.CreateMeetUp(meetUp);
            var meetUpFound = await _repository.GetMeetUp(meetUp.Name);
            if (meetUpFound == null) throw new Exception("Meet Up has not been added to repository");
            string? userEmail = HttpContext.Session.GetString("username");
            if(string.IsNullOrEmpty(userEmail)) throw new Exception("You must be logged");
            User? userLogged = await _userRepository.GetUser(userEmail);
            if(userLogged == null) throw new Exception("User has not been found");
            UserMeetUpOwner owner = new UserMeetUpOwner()
            {
                UserId = userLogged.Id,
                MeetUpId = meetUpFound.Id,
                DateCreated = DateTime.Now
            };
            await _repository.AssignMeetUpOwner(owner);
            BasicMeetUpModel meetUpResult = _mappingHelper.ConvertTo<BasicMeetUpModel, MeetUp>(meetUpFound);
            return Ok(meetUpResult);
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
            if (model == null || model.Id <= 0 || string.IsNullOrEmpty(model.Name) || model.LocationId <= 0) throw new ArgumentNullException("Model is invalid");
            if (model.FinalDate < DateTime.Now || model.FinalDate < model.InitialDate) throw new Exception("Final date is not valid");
            if (model.InitialDate < DateTime.Now) throw new Exception("Initial date is not valid");
            Location? locationFound = await _locationRepo.GetLocation(model.LocationId);
            if (locationFound == null) throw new Exception("Location does not exist");
            MeetUp? meetUpFound = await _repository.GetMeetUp(model.Id);
            if (meetUpFound == null) return NotFound("Meet Up has not been found");
            meetUpFound.Name = model.Name;
            meetUpFound.InitialDate = model.InitialDate;
            meetUpFound.FinalDate = model.FinalDate;
            meetUpFound.LocationId = model.LocationId;
            await _repository.UpdateMeetUp(meetUpFound);
            meetUpFound = await _repository.GetMeetUp(model.Id);
            if (meetUpFound == null) return NotFound("Meet Up has not been found");
            BasicMeetUpModel meetUpResult = _mappingHelper.ConvertTo<BasicMeetUpModel, MeetUp>(meetUpFound);
            return Ok(meetUpResult);
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
            if (id <= 0) throw new ArgumentOutOfRangeException("Id is out of range");
            MeetUp? meetUpFound = await _repository.GetMeetUp(id);
            if (meetUpFound == null) return NotFound("Meet up has not been found");
            await _repository.DeleteMeetUp(meetUpFound);
            return Ok("Meet up has been deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetAllMeetUps"), AllowAnonymous]
    public async Task<IActionResult> GetAllMeetUps()
    {
        try
        {
            List<MeetUp> meetUpList = await _repository.GetMeetUps();
            List<BasicMeetUpModel> result = _mappingHelper.ConvertTo<List<BasicMeetUpModel>, List<MeetUp>>(meetUpList);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetMeetUpByLocation/{locationId}"), AllowAnonymous]
    public async Task<IActionResult> GetMeetUpByLocation(int locationId)
    {
        try
        {
            if (locationId <= 0) throw new ArgumentOutOfRangeException("Location is not valid");
            Location? locationFound = await _locationRepo.GetLocation(locationId);
            if (locationFound == null) throw new Exception("Location does not exist");
            List<MeetUp> meetUpList = await _repository.GetMeetUpsByLocation(locationId);
            List<BasicMeetUpModel> result = _mappingHelper.ConvertTo<List<BasicMeetUpModel>, List<MeetUp>>(meetUpList);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPost("AddEvent")]
    public async Task<IActionResult> AddEvent([FromBody] AddEventModel model)
    {
        try
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.StartHour) || model.MeetUpId <= 0) throw new Exception("Model is invalid");
            if (!Regex.Match(model.StartHour, @"^([01][0-9]|2[0-3]):([0-5][0-9])$").Success) throw new Exception("Invalid hour format. Valid format {HH:MM}");
            MeetUp? meetUpFound = await _repository.GetMeetUp(model.MeetUpId);
            if (meetUpFound == null) throw new Exception("Meet up does not exist");
            Event meetUpEvent = _mappingHelper.ConvertTo<Event, AddEventModel>(model);
            await _repository.CreateEvent(meetUpEvent);
            Event? eventFound = await _repository.GetEvent(model.Name);
            if (eventFound == null) throw new Exception("Event has not been added to the repository");
            BasicEventModel eventResult = _mappingHelper.ConvertTo<BasicEventModel, Event>(eventFound);
            return Ok(eventResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPut("UpdateEvent")]
    public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventModel model)
    {
        try
        {
            if (model.Id <= 0 || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.StartHour) || model.MeetUpId <= 0) throw new Exception("Model is invalid");
            if (!Regex.Match(model.StartHour, @"^([01][0-9]|2[0-3]):([0-5][0-9])$").Success) throw new Exception("Invalid hour format. Valid format {HH:MM}");
            Event? eventFound = await _repository.GetEvent(model.Id);
            if (eventFound == null) return NotFound("Event has not been found");
            MeetUp? meetUpFound = await _repository.GetMeetUp(model.MeetUpId);
            if (meetUpFound == null) throw new Exception("Meet up does not exist");
            eventFound.Name = model.Name;
            eventFound.StartHour = model.StartHour;
            eventFound.Details = model.Details;
            eventFound.Status = model.Status;
            eventFound.MeetUpId = model.MeetUpId;
            await _repository.UpdateEvent(eventFound);
            eventFound = await _repository.GetEvent(model.Id);
            if (eventFound == null) return NotFound("Event has not been found");
            BasicEventModel eventResult = _mappingHelper.ConvertTo<BasicEventModel, Event>(eventFound);
            return Ok(eventResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpDelete("DeleteEvent/{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        try
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id is out of range");
            Event? eventFound = await _repository.GetEvent(id);
            if (eventFound == null) return NotFound("Event has not been found");
            await _repository.DeleteEvent(eventFound);
            return Ok("Event has been deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetEventsByMeetUp/{meetUpId}")]
    public async Task<IActionResult> GetEventsByMeetUp(int meetUpId)
    {
        try
        {
            if (meetUpId <= 0) throw new ArgumentOutOfRangeException("Id is out of range");
            MeetUp? meetUpFound = await _repository.GetMeetUp(meetUpId);
            if (meetUpFound == null) return NotFound("Meet Up does not exist");
            List<Event> events = await _repository.GetEventsByMeetUp(meetUpId);
            List<BasicEventModel> eventResult = _mappingHelper.ConvertTo<List<BasicEventModel>, List<Event>>(events);
            return Ok(eventResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetEvent/{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        try
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id is out of range");
            Event? eventFound = await _repository.GetEvent(id);
            if (eventFound == null) return NotFound("Event has not been found");
            BasicEventModel eventResult = _mappingHelper.ConvertTo<BasicEventModel, Event>(eventFound);
            return Ok(eventResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    public async Task<IActionResult> RegisterAttendance(UserAttendanceModel model)
    {
        try
        {
            if(model.UserId <= 0 || model.MeetUpId <= 0) throw new ArgumentOutOfRangeException("Model invalid");
            MeetUp? meetUpFound = await _repository.GetMeetUp(model.MeetUpId);
            if(meetUpFound == null) return NotFound("Meet Up has not been found");
            User? userFound = await _userRepository.GetUser(model.UserId);
            if(userFound == null) return NotFound("User has not been found");
            UserMeetUpAssistant userAssistant = new UserMeetUpAssistant()
            {
                UserId = userFound.Id,
                MeetUpId = meetUpFound.Id,
                ReservedAt = DateTime.Now
            };
            await _repository.RegisterAttendance(userAssistant);
            return Ok("User has been added to event participants");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }
}