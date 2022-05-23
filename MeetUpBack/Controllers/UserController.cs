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
public class UserController : ControllerBase
{
    private readonly IAuthRepository _repository;
    private readonly IMappingHelper _mappingHelper;
    private readonly IPasswordManagerHelper _passwordManager;
    private readonly ITokenFactoryHelper _tokenFactory;

    public UserController(IAuthRepository repository,
                            IMappingHelper mappingHelper,
                            IPasswordManagerHelper passwordManager,
                            ITokenFactoryHelper tokenFactory)
    {
        _repository = repository;
        _mappingHelper = mappingHelper;
        _passwordManager = passwordManager;
        _tokenFactory = tokenFactory;
    }

    [HttpPost("CreateUser"), AllowAnonymous]
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
            BasicUserModel userResult = _mappingHelper.ConvertTo<BasicUserModel,User>(userFound);
            return Ok(userResult);
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
            userFound = await _repository.GetUser(model.Id);
            if (userFound == null) return NotFound("User has not been found.");
            BasicUserModel userResult = _mappingHelper.ConvertTo<BasicUserModel,User>(userFound);
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
            User? userFound = await _repository.GetUser(id); 
            if(userFound == null) return NotFound("User has not been found");
            BasicUserModel user = _mappingHelper.ConvertTo<BasicUserModel,User>(userFound);
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPut("ModifyPassword")]
    public async Task<IActionResult> ModifyPassword(UpdatePasswordModel model)
    {
        try
        {
            if(model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.LastPassword)) throw new Exception("Model is not valid");
            User? userFound = await _repository.GetUser(model.Email);
            if(userFound == null) return NotFound("User has not been found");
            bool IsValidPassword = _passwordManager.IsValidHashCode(model.LastPassword,userFound.PasswordHash);
            if(!IsValidPassword) return BadRequest("Last password does not match");
            string passwordHash = _passwordManager.GenerateHashCode(model.Password);
            userFound.PasswordHash = passwordHash;
            await _repository.UpdateUser(userFound);
            return Ok("Password has been modified successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpPost("Login"),AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {
            if(model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)) throw new Exception("Model is not valid");
            User? userFound = await _repository.GetUser(model.Email);
            if(userFound == null) return NotFound("User has not been found");
            bool isValidPassword = _passwordManager.IsValidHashCode(model.Password,userFound.PasswordHash);
            if(!isValidPassword) return BadRequest("Invalid email or password");
            TokenModel tokenModel = _tokenFactory.GenerateToken(userFound);
            Session? sessionFound = await _repository.GetSession(userFound.Id);
            if(sessionFound != null)
            {
                sessionFound.Token = tokenModel.Token;
                sessionFound.Created = DateTime.Now;
                sessionFound.Expires = tokenModel.ExpiresIn;
                await _repository.UpdateSession(sessionFound);
            }else
            {
                Session newSession = new Session()
                {
                    Token = tokenModel.Token,
                    Created = DateTime.Now,
                    Expires = tokenModel.ExpiresIn,
                    UserId = userFound.Id
                };
                await _repository.InsertSession(newSession);
            }
            return Ok(tokenModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }

    [HttpGet("Logout/{id}")]
    public async Task<IActionResult> Logout(int id)
    {
        try
        {
            if(id <= 0) throw new ArgumentOutOfRangeException("Id is out of range");
            User? userFound = await _repository.GetUser(id);
            if(userFound == null) return NotFound("User has not been found");
            Session? sessionFound = await _repository.GetSession(id);
            if(sessionFound == null) return NotFound("User has not have session");
            await _repository.DeleteSession(sessionFound);
            return Ok("Session has been closed successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex);
        }
    }
}