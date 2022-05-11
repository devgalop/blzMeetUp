using System;
using System.Collections.Generic;
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

    [Fact]
    public async Task UpdateMeetUp_WithModelNull_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = null!;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithInvalidId_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 0,
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithInvalidName_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = string.Empty,
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithInvalidLocation_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 0
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithInvalidFinalDate_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(2),
            FinalDate = DateTime.MinValue,
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithInvalidInitialDate_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = "Event Test",
            InitialDate = DateTime.MinValue,
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithUnexistLocation_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(1),
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync((Location?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithUnexistMeetUp_ReturnsNotFound()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(1),
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithValidModel_ReturnsOk()
    {
        UpdateMeetUpModel model = new UpdateMeetUpModel()
        {
            Id = 1,
            Name = "Event Test",
            InitialDate = DateTime.Now.AddDays(1),
            FinalDate = DateTime.Now.AddDays(2),
            LocationId = 1
        };
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetUp_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.DeleteMeetUp(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetUp_WithUnexistMeetUp_ReturnsNotFound()
    {
        int id = 1;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);
        var locationRepositoryStub = new Mock<ILocationRepository>();
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.DeleteMeetUp(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetUp_WithValidModel_ReturnsOk()
    {
        int id = 1;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.DeleteMeetUp(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMeetUpByLocation_WithInvalidLocation_ReturnsBadRequest()
    {
        int locationId = 0;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.GetMeetUpsByLocation(It.IsAny<int>()))
                            .ReturnsAsync(new List<MeetUp>());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.GetMeetUpByLocation(locationId);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetMeetUpByLocation_WithUnexistLocation_ReturnsBadRequest()
    {
        int locationId = 1;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.GetMeetUpsByLocation(It.IsAny<int>()))
                            .ReturnsAsync(new List<MeetUp>());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync((Location?)null);
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.GetMeetUpByLocation(locationId);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetMeetUpByLocation_WithValidLocation_ReturnsOk()
    {
        int locationId = 1;
        var meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        meetUpRepositoryStub.Setup(x => x.GetMeetUpsByLocation(It.IsAny<int>()))
                            .ReturnsAsync(new List<MeetUp>());
        var locationRepositoryStub = new Mock<ILocationRepository>();
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        var mappingHelperStub = new Mock<IMappingHelper>();
        var controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);

        var result = await controller.GetMeetUpByLocation(locationId);

        Assert.IsType<OkObjectResult>(result);
    }

    
}