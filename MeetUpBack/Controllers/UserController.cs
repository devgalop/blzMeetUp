using MeetUpBack.Data.Entities;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MeetUpBack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthRepository _repository;
    private readonly IMappingHelper _mappingHelper;
    private readonly IPasswordManagerHelper _passwordManager;

    public UserController(IAuthRepository repository,
                            IMappingHelper mappingHelper,
                            IPasswordManagerHelper passwordManager)
    {
        _repository = repository;
        _mappingHelper = mappingHelper;
        _passwordManager = passwordManager;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(AddUserModel model)
    {
        try
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Name) || model.RoleId <= 0) throw new Exception("Model is invalid.");
            Role? roleFound = await _repository.GetRole(model.RoleId);
            if (roleFound == null) throw new Exception("Role has not been found.");
            model.PasswordHash = _passwordManager.GenerateHashCode(model.Password);
            User user = _mappingHelper.ConvertTo<User, AddUserModel>(model);
            await _repository.RegisterUser(user);
            User? userFound = await _repository.GetUser(model.Email);
            if (userFound == null) throw new Exception("User has not been registered into the repository.");
            return Ok(userFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UpdateUserModel model)
    {
        try
        {
            if (model == null || model.Id <= 0 || string.IsNullOrEmpty(model.Name) || model.RoleId <= 0) throw new Exception("Model is invalid.");
            Role? roleFound = await _repository.GetRole(model.RoleId);
            if (roleFound == null) throw new Exception("Role has not been found.");
            User? userFound = await _repository.GetUser(model.Id);
            if (userFound == null) return NotFound("User has not been found.");
            userFound.Name = model.Name;
            userFound.LastName = model.LastName;
            userFound.Status = model.Status;
            userFound.RoleId = model.RoleId;
            await _repository.UpdateUser(userFound);
            return Ok(userFound);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            if (id <= 0) throw new Exception("Model is invalid.");
            User? userFound = await _repository.GetUser(id);
            if (userFound == null) return NotFound("User has not been found.");
            await _repository.DeleteUser(userFound);
            return Ok("User has been deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("GetUser/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            if (id <= 0) throw new Exception("Model is invalid.");
            return Ok(await _repository.GetUser(id));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

}