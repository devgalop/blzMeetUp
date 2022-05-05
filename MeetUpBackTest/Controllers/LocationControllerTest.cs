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
}