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

public class LocationControllerTest
{
    private Mock<ILocationRepository> _repositoryStub;
    private Mock<IMappingHelper> _mappingHelperStub;
    private LocationManagerController _controller;

    public LocationControllerTest()
    {
        _repositoryStub = new Mock<ILocationRepository>();
        _mappingHelperStub = new Mock<IMappingHelper>();
        _controller = new LocationManagerController(_repositoryStub.Object, _mappingHelperStub.Object);
    }

    [Fact]
    public async Task RegisterCountry_WithModelNull_ReturnsBadRequest()
    {
        //Arrange
        AddCountryModel model = null!;
        
        //Act
        var result = await _controller.RegisterCountry(model);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCountry_WithCountryNameInvalid_ReturnsBadRequest()
    {
        //Arrange
        AddCountryModel model = new AddCountryModel(){Name = string.Empty};
        
        //Act
        var result = await _controller.RegisterCountry(model);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCountry_WithProblemsToAdd_ReturnsBadRequest()
    {
        //Arrange
        AddCountryModel model = new AddCountryModel()
        {
            Name = "CountryTest"
        };
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<string>())).ReturnsAsync((Country?)null);
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<Country,AddCountryModel>(It.IsAny<AddCountryModel>()))
            .Returns(new Country());
        
        //Act
        var result = await _controller.RegisterCountry(model);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCountry_WithModelOk_ReturnsOk()
    {
        //Arrange
        AddCountryModel model = new AddCountryModel()
        {
            Name = "CountryTest"
        };
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<string>())).ReturnsAsync(new Country());
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<Country,AddCountryModel>(It.IsAny<AddCountryModel>()))
            .Returns(new Country());
        
        //Act
        var result = await _controller.RegisterCountry(model);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCountry_WithInvalidId_ReturnsBadRequest()
    {
        //Arrange
        int id = 0;

        //Act
        var result = await _controller.GetCountry(id);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetCountry_WithUnexistCountry_ReturnsNotFound()
    {
        //Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync((Country?)null);

        //Act
        var result = await _controller.GetCountry(id);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCountry_WithValidModel_ReturnsOk()
    {
        //Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());

        //Act
        var result = await _controller.GetCountry(id);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithModelNull_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = null!;
        
        // Act
        var result = await _controller.RegisterCity(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithInvalidName_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = new AddCityModel()
        {
            Name = string.Empty,
            CountryId = 1
        };
        
        // Act
        var result = await _controller.RegisterCity(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithInvalidCountry_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = new AddCityModel()
        {
            Name = "CiudadPrueba",
            CountryId = 0
        };
        
        // Act
        var result = await _controller.RegisterCity(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithUnexistCountry_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = new AddCityModel()
        {
            Name = "CiudadPrueba",
            CountryId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync((Country?)null);
        
        // Act
        var result = await _controller.RegisterCity(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithProblemsToAdd_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = new AddCityModel()
        {
            Name = "CiudadPrueba",
            CountryId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<string>())).ReturnsAsync((City?)null);
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        
        // Act
        var result = await _controller.RegisterCity(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithValidModel_ReturnsOk()
    {
        // Arrange
        AddCityModel model = new AddCityModel()
        {
            Name = "CiudadPrueba",
            CountryId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<string>())).ReturnsAsync(new City());
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        
        // Act
        var result = await _controller.RegisterCity(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCity_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;

        // Act
        var result = await _controller.GetCity(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetCity_WithUnexistCity_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync((City?)null);

        // Act
        var result = await _controller.GetCity(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCity_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());

        // Act
        var result = await _controller.GetCity(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithModelNull_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = null!;

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithInvalidCityId_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 0
        };

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithInvalidAddress_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = string.Empty,
            Capacity = 100,
            CityId = 1
        };

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithInvalidCapacity_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 0,
            CityId = 1
        };

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithUnexistCity_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync((City?)null);

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithProblemsToAdd_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<string>())).ReturnsAsync((Location?) null);
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithValidModel_ReturnsOk()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<string>())).ReturnsAsync(new Location());
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());

        // Act
        var result = await _controller.AddLocation(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithModelNull_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = null!;

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithInvalidCityId_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 1,
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 0
        };

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithInvalidAddress_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 1,
            Name = "LocationTest",
            Address = string.Empty,
            Capacity = 100,
            CityId = 1
        };

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithInvalidCapacity_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 1,
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 0,
            CityId = 1
        };

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 0,
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithUnexistCity_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 1,
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync((City?)null);

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithUnexistLocation_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 1,
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync((Location?) null);
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithValidModel_ReturnsOk()
    {
        // Arrange
        UpdateLocationModel model = new UpdateLocationModel()
        {
            Id = 1,
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        _repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        _mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());

        // Act
        var result = await _controller.UpdateLocation(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

[Fact]
    public async Task DeleteLocation_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;

        // Act
        var result = await _controller.DeleteLocation(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);                
    }

    [Fact]
    public async Task DeleteLocation_WithUnexistLocation_ReturnsBadRequest()
    {
        // Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync((Location?)null);

        // Act
        var result = await _controller.DeleteLocation(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);                
    }

    [Fact]
    public async Task DeleteLocation_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        _repositoryStub.Setup(repo => repo.DeleteLocation(It.IsAny<Location>()));

        // Act
        var result = await _controller.DeleteLocation(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);  
    }

    [Fact]
    public async Task GetLocation_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;

        // Act
        var result = await _controller.GetLocation(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);                
    }

    [Fact]
    public async Task GetLocation_WithUnexistLocation_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync((Location?)null);

        // Act
        var result = await _controller.GetLocation(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);                
    }

    [Fact]
    public async Task GetLocation_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        _repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());

        // Act
        var result = await _controller.GetLocation(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);                
    }
}