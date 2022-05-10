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
    private readonly IMappingHelper _mappingHelper;

    public MeetUpManagerController(IMeetUpRepository repository,
                                    IMappingHelper mappingHelper)
    {
        _repository = repository;
        _mappingHelper = mappingHelper;
    }

    [HttpPost("Create")]
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
}