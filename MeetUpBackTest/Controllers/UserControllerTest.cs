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
    [Fact]
    public async Task CreateUser_WithModelNull_ReturnsBadRequest()
    {
        AddUserModel model = null!;
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync((Role?)null);
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync((Role?)null);
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync((User?)null);
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

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
        var repositoryStub = new Mock<IAuthRepository>();
        repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                        .ReturnsAsync(new Role());
        repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                        .ReturnsAsync(new User());
        repositoryStub.Setup(repo => repo.RegisterUser(It.IsAny<User>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<User, AddUserModel>(It.IsAny<AddUserModel>()))
            .Returns(new User());
        var passwordHelperStub = new Mock<IPasswordManagerHelper>();
        var controller = new UserController(repositoryStub.Object, mappingHelperStub.Object, passwordHelperStub.Object);

        var result = await controller.CreateUser(model);

        Assert.IsType<OkObjectResult>(result);
    }

}