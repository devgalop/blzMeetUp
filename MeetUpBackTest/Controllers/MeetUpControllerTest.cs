using System;
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

public class MeetUpControllerTest
{
    [Fact]
    public async Task CreateMeetUp_WithModelNull_ReturnsBadRequest()
    {
        AddMeetUpModel model = null!;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithInvalidName_ReturnsBadRequest()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = string.Empty,
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(3),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithInvalidLocation_ReturnsBadRequest()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(3),
            LocationId = 0
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithInvalidFinalDate_ReturnsBadRequest()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.MinValue,
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithInvalidInitialDate_ReturnsBadRequest()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = "Event Test",
            InitialDate = DateTime.MinValue,
            FinalDate = DateTime.Now,
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithUnexistLocation_ReturnsBadRequest()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(3),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync((Location?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithProblemsToAdd_ReturnsBadRequest()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(3),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<string>())).ReturnsAsync((MeetUp?)null);
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateMeetUp_WithValidModel_ReturnsOk()
    {
        AddMeetUpModel model = new AddMeetUpModel()
        {
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(3),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<string>()))
                            .ReturnsAsync(new MeetUp());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.CreateMeetUp(model);

        Assert.IsType<OkObjectResult>(result);
    }
}