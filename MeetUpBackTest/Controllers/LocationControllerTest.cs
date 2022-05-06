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
    [Fact]
    public async Task RegisterCountry_WithModelNull_ReturnsBadRequest()
    {
        //Arrange
        AddCountryModel model = null!;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.InsertCountry(It.IsAny<Country>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Country,AddCountryModel>(It.IsAny<AddCountryModel>()))
            .Returns(new Country());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        //Act
        var result = await controller.RegisterCountry(model);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCountry_WithCountryNameInvalid_ReturnsBadRequest()
    {
        //Arrange
        AddCountryModel model = new AddCountryModel(){Name = string.Empty};
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.InsertCountry(It.IsAny<Country>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Country,AddCountryModel>(It.IsAny<AddCountryModel>()))
            .Returns(new Country());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        //Act
        var result = await controller.RegisterCountry(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<string>())).ReturnsAsync((Country?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Country,AddCountryModel>(It.IsAny<AddCountryModel>()))
            .Returns(new Country());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        //Act
        var result = await controller.RegisterCountry(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<string>())).ReturnsAsync(new Country());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Country,AddCountryModel>(It.IsAny<AddCountryModel>()))
            .Returns(new Country());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        //Act
        var result = await controller.RegisterCountry(model);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCountry_WithInvalidId_ReturnsBadRequest()
    {
        //Arrange
        int id = 0;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        //Act
        var result = await controller.GetCountry(id);

        //Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetCountry_WithUnnexistCountry_ReturnsNotFound()
    {
        //Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync((Country?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        //Act
        var result = await controller.GetCountry(id);

        //Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCountry_WithValidModel_ReturnsOk()
    {
        //Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        //Act
        var result = await controller.GetCountry(id);

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithModelNull_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = null!;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.InsertCity(It.IsAny<City>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        // Act
        var result = await controller.RegisterCity(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.InsertCity(It.IsAny<City>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        // Act
        var result = await controller.RegisterCity(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.InsertCity(It.IsAny<City>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        // Act
        var result = await controller.RegisterCity(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task RegisterCity_WithUnnexistCountry_ReturnsBadRequest()
    {
        // Arrange
        AddCityModel model = new AddCityModel()
        {
            Name = "CiudadPrueba",
            CountryId = 1
        };
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync((Country?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        // Act
        var result = await controller.RegisterCity(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<string>())).ReturnsAsync((City?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        // Act
        var result = await controller.RegisterCity(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCountry(It.IsAny<int>())).ReturnsAsync(new Country());
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<string>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<City,AddCityModel>(It.IsAny<AddCityModel>()))
            .Returns(new City());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);
        
        // Act
        var result = await controller.RegisterCity(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCity_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<string>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.GetCity(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetCity_WithUnnexistCity_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync((City?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.GetCity(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetCity_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.GetCity(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithModelNull_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = null!;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddLocation_WithUnnexistCity_ReturnsBadRequest()
    {
        // Arrange
        AddLocationModel model = new AddLocationModel()
        {
            Name = "LocationTest",
            Address = "AddressTest",
            Capacity = 100,
            CityId = 1
        };
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync((City?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<string>())).ReturnsAsync((Location?) null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<string>())).ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,AddLocationModel>(It.IsAny<AddLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.AddLocation(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithModelNull_ReturnsBadRequest()
    {
        // Arrange
        UpdateLocationModel model = null!;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithUnnexistCity_ReturnsBadRequest()
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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync((City?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateLocation_WithUnnexistLocation_ReturnsBadRequest()
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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync((Location?) null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

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
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetCity(It.IsAny<int>())).ReturnsAsync(new City());
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Location,UpdateLocationModel>(It.IsAny<UpdateLocationModel>()))
            .Returns(new Location());
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.UpdateLocation(model);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

[Fact]
    public async Task DeleteLocation_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.DeleteLocation(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);                
    }

    [Fact]
    public async Task DeleteLocation_WithUnnexistLocation_ReturnsBadRequest()
    {
        // Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync((Location?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.DeleteLocation(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);                
    }

    [Fact]
    public async Task DeleteLocation_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        repositoryStub.Setup(repo => repo.DeleteLocation(It.IsAny<Location>()));
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.DeleteLocation(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);  
    }

    [Fact]
    public async Task GetLocation_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int id = 0;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.GetLocation(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);                
    }

    [Fact]
    public async Task GetLocation_WithUnnexistLocation_ReturnsNotFound()
    {
        // Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync((Location?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.GetLocation(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);                
    }

    [Fact]
    public async Task GetLocation_WithValidId_ReturnsOk()
    {
        // Arrange
        int id = 1;
        var repositoryStub = new Mock<ILocationRepository>();
        repositoryStub.Setup(repo => repo.GetLocation(It.IsAny<int>())).ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new LocationManagerController(repositoryStub.Object, mappingHelperStub.Object);

        // Act
        var result = await controller.GetLocation(id);

        // Assert
        Assert.IsType<OkObjectResult>(result);                
    }
}