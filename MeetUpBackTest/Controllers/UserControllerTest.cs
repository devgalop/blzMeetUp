using System.Threading.Tasks;
using MeetUpBack.Controllers;
using MeetUpBack.Data.Entities;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace MeetUpBackTest.Controllers;

public class UserControllerTest
{
    private Mock<IAuthRepository> _repositoryStub;
    private Mock<IMappingHelper> _mappingHelperStub;
    private Mock<ITokenFactoryHelper> _tokenFactoryStub;
    private Mock<IPasswordManagerHelper> _passwordHelperStub;
    private UserController _controller;

    public UserControllerTest()
    {
        _repositoryStub = new Mock<IAuthRepository>();
        _mappingHelperStub = new Mock<IMappingHelper>();
        _tokenFactoryStub = new Mock<ITokenFactoryHelper>();
        _passwordHelperStub = new Mock<IPasswordManagerHelper>();
        _controller = new UserController(_repositoryStub.Object, _mappingHelperStub.Object, _passwordHelperStub.Object,_tokenFactoryStub.Object);
    }

    [Fact]
    public async Task CreateUser_WithModelNull_ReturnsBadRequest()
    {
        AddUserModel model = null!;

        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithEmptyEmail_ReturnsBadRequest()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = "test",
            LastName = "test",
            Email = string.Empty,
            Password = "123456",
            RoleId = 1
        };

        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithEmptyPassword_ReturnsBadRequest()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = "test",
            LastName = "test",
            Email = "test@example.com",
            Password = string.Empty,
            RoleId = 1
        };

        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithEmptyName_ReturnsBadRequest()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = string.Empty,
            LastName = "test",
            Email = "test@example.com",
            Password = "123456",
            RoleId = 1
        };
        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithInvalidRole_ReturnsBadRequest()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = "test",
            LastName = "test",
            Email = "test@example.com",
            Password = "123456",
            RoleId = 0
        };

        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithUnexistRole_ReturnsBadRequest()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = "test",
            LastName = "test",
            Email = "test@example.com",
            Password = "123456",
            RoleId = 1
        };

        _repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync((Role?)null);
        
        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithProblemsToAdd_ReturnsBadRequest()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = "test",
            LastName = "test",
            Email = "test@example.com",
            Password = "123456",
            RoleId = 1
        };
        
        _repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync((User?)null);
        _repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());

        var result = await _controller.CreateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateUser_WithValidModel_ReturnsOk()
    {
        AddUserModel model = new AddUserModel()
        {
            Name = "test",
            LastName = "test",
            Email = "test@example.com",
            Password = "123456",
            RoleId = 1
        };
        _repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        _repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());

        var result = await _controller.CreateUser(model);

        Assert.IsType<OkObjectResult>(result);
    }

}