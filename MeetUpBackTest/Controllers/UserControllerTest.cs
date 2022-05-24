using System.Threading.Tasks;
using MeetUpBack.Controllers;
using MeetUpBack.Data.Entities;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Http;
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
    private Mock<HttpContext> mockHttpContext;
    private MockHttpSession mockSession;
    private UserController _controller;

    public UserControllerTest()
    {
        _repositoryStub = new Mock<IAuthRepository>();
        _mappingHelperStub = new Mock<IMappingHelper>();
        _tokenFactoryStub = new Mock<ITokenFactoryHelper>();
        _passwordHelperStub = new Mock<IPasswordManagerHelper>();
        mockHttpContext = new Mock<HttpContext>();
        mockSession = new MockHttpSession();
        _controller = new UserController(_repositoryStub.Object, _mappingHelperStub.Object, _passwordHelperStub.Object,_tokenFactoryStub.Object);
        mockHttpContext.Setup(s => s.Session).Returns(mockSession);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;
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

    [Fact]
    public async Task UpdateUser_WithModelNull_ReturnsBadRequest()
    {
        UpdateUserModel model = null!;

        var result = await _controller.UpdateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_WithInvalidId_ReturnsBadRequest()
    {
        UpdateUserModel model = new UpdateUserModel()
        {
            Id = 0,
            Name = "test",
            LastName = "test",
            RoleId = 1
        };

        var result = await _controller.UpdateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_WithInvalidRoleId_ReturnsBadRequest()
    {
        UpdateUserModel model = new UpdateUserModel()
        {
            Id = 1,
            Name = "test",
            LastName = "test",
            RoleId = 0
        };

        var result = await _controller.UpdateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_WithEmptyName_ReturnsBadRequest()
    {
        UpdateUserModel model = new UpdateUserModel()
        {
            Id = 1,
            Name = string.Empty,
            LastName = "test",
            RoleId = 1
        };

        var result = await _controller.UpdateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_WithUnexistRole_ReturnsBadRequest()
    {
        UpdateUserModel model = new UpdateUserModel()
        {
            Id = 1,
            Name = "test",
            LastName = "test",
            RoleId = 1
        };

        _repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                    .ReturnsAsync((Role?)null);

        var result = await _controller.UpdateUser(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_WithUnexistUser_ReturnsNotFound()
    {
        UpdateUserModel model = new UpdateUserModel()
        {
            Id = 1,
            Name = "test",
            LastName = "test",
            RoleId = 1
        };

        _repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                    .ReturnsAsync(new Role());
        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<int>()))
                    .ReturnsAsync((User?)null);

        var result = await _controller.UpdateUser(model);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUser_WithValidModel_ReturnsOk()
    {
        UpdateUserModel model = new UpdateUserModel()
        {
            Id = 1,
            Name = "test",
            LastName = "test",
            RoleId = 1
        };

        _repositoryStub.Setup(repo => repo.GetRole(It.IsAny<int>()))
                    .ReturnsAsync(new Role());
        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<int>()))
                    .ReturnsAsync(new User());
        _repositoryStub.Setup(repo => repo.UpdateUser(It.IsAny<User>()));

        var result = await _controller.UpdateUser(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;

        var result = await _controller.DeleteUser(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_WithUnexistUser_ReturnsNotFound()
    {
        int id = 1;

        _repositoryStub.Setup(repo => repo.GetUser(id))
                    .ReturnsAsync((User?)null);

        var result = await _controller.DeleteUser(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_WithValidModel_ReturnsOk()
    {
        int id = 1;

        _repositoryStub.Setup(repo => repo.GetUser(id))
                    .ReturnsAsync(new User());

        var result = await _controller.DeleteUser(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;

        var result = await _controller.GetUser(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetUser_WithUnexistUser_ReturnsNotFound()
    {
        int id = 1;

        _repositoryStub.Setup(repo => repo.GetUser(id))
                    .ReturnsAsync((User?)null);

        var result = await _controller.GetUser(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetUser_WithValidModel_ReturnsOk()
    {
        int id = 1;

        _repositoryStub.Setup(repo => repo.GetUser(id))
                    .ReturnsAsync(new User());

        var result = await _controller.GetUser(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithModelNull_ReturnsBadRequest()
    {
        UpdatePasswordModel model = null!;

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithEmptyEmail_ReturnsBadRequest()
    {
        UpdatePasswordModel model = new UpdatePasswordModel()
        {
            Email = string.Empty,
            Password = "test",
            LastPassword = "test",
        };

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithEmptyPassword_ReturnsBadRequest()
    {
        UpdatePasswordModel model = new UpdatePasswordModel()
        {
            Email = "test",
            Password = string.Empty,
            LastPassword = "test",
        };

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithEmptyLastPassword_ReturnsBadRequest()
    {
        UpdatePasswordModel model = new UpdatePasswordModel()
        {
            Email = "test",
            Password = "test",
            LastPassword = string.Empty,
        };

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithUnexistUser_ReturnsNotFound()
    {
        UpdatePasswordModel model = new UpdatePasswordModel()
        {
            Email = "test",
            Password = "test",
            LastPassword = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync((User?)null);

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithIncorrectLastPassword_ReturnsBadRequest()
    {
        UpdatePasswordModel model = new UpdatePasswordModel()
        {
            Email = "test",
            Password = "test",
            LastPassword = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync(new User());
        _passwordHelperStub.Setup(pass => pass.IsValidHashCode(It.IsAny<string>(),It.IsAny<string>()))
                    .Returns(false);

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task ModifyPassword_WithValidModel_ReturnsOk()
    {
        UpdatePasswordModel model = new UpdatePasswordModel()
        {
            Email = "test",
            Password = "test",
            LastPassword = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync(new User());
        _repositoryStub.Setup(repo => repo.UpdateUser(It.IsAny<User>()));
        _passwordHelperStub.Setup(pass => pass.IsValidHashCode(It.IsAny<string>(),It.IsAny<string>()))
                    .Returns(true);
        _passwordHelperStub.Setup(pass => pass.GenerateHashCode(It.IsAny<string>()))
                    .Returns(It.IsAny<string>());

        var result = await _controller.ModifyPassword(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithModelNull_ReturnsBadRequest()
    {
        LoginModel model = null!;

        var result = await _controller.Login(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithEmptyEmail_ReturnsBadRequest()
    {
        LoginModel model = new LoginModel()
        {
            Email = string.Empty,
            Password = "test"
        };

        var result = await _controller.Login(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithEmptyPassword_ReturnsBadRequest()
    {
        LoginModel model = new LoginModel()
        {
            Email = "test",
            Password = string.Empty
        };

        var result = await _controller.Login(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithUnexistUser_ReturnsNotFound()
    {
        LoginModel model = new LoginModel()
        {
            Email = "test",
            Password = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync((User?)null);

        var result = await _controller.Login(model);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithIncorrectPassword_ReturnsBadRequest()
    {
        LoginModel model = new LoginModel()
        {
            Email = "test",
            Password = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync(new User());
        _passwordHelperStub.Setup(pass => pass.IsValidHashCode(It.IsAny<string>(),It.IsAny<string>())).Returns(false);

        var result = await _controller.Login(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithExistsSession_ReturnsOk()
    {
        LoginModel model = new LoginModel()
        {
            Email = "test",
            Password = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync(new User());
        _repositoryStub.Setup(repo => repo.GetSession(It.IsAny<int>()))
                    .ReturnsAsync(new Session());
        _repositoryStub.Setup(repo => repo.UpdateSession(It.IsAny<Session>()));
        _passwordHelperStub.Setup(pass => pass.IsValidHashCode(It.IsAny<string>(),It.IsAny<string>())).Returns(true);
        _tokenFactoryStub.Setup(token => token.GenerateToken(It.IsAny<User>()))
                    .Returns(new TokenModel());

        var result = await _controller.Login(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Login_WithUnexistSession_ReturnsOk()
    {
        LoginModel model = new LoginModel()
        {
            Email = "test",
            Password = "test"
        };

        _repositoryStub.Setup(repo => repo.GetUser(It.IsAny<string>()))
                    .ReturnsAsync(new User());
        _repositoryStub.Setup(repo => repo.GetSession(It.IsAny<int>()))
                    .ReturnsAsync((Session?)null);
        _repositoryStub.Setup(repo => repo.InsertSession(It.IsAny<Session>()));
        _passwordHelperStub.Setup(pass => pass.IsValidHashCode(It.IsAny<string>(),It.IsAny<string>())).Returns(true);
        _tokenFactoryStub.Setup(token => token.GenerateToken(It.IsAny<User>()))
                    .Returns(new TokenModel());

        var result = await _controller.Login(model);

        Assert.IsType<OkObjectResult>(result);
    }

}