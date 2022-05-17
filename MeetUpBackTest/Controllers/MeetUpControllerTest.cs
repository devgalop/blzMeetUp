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
    private Mock<IMeetUpRepository> meetUpRepositoryStub;
    private Mock<ILocationRepository> locationRepositoryStub;
    private Mock<IMappingHelper> mappingHelperStub;
    private MeetUpManagerController controller;

    public MeetUpControllerTest()
    {
        meetUpRepositoryStub = new Mock<IMeetUpRepository>();
        locationRepositoryStub = new Mock<ILocationRepository>();
        mappingHelperStub = new Mock<IMappingHelper>();
        controller = new MeetUpManagerController(meetUpRepositoryStub.Object,locationRepositoryStub.Object,mappingHelperStub.Object);
    }

    [Fact]
    public async Task CreateMeetUp_WithModelNull_ReturnsBadRequest()
    {
        AddMeetUpModel model = null!;

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
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync((Location?)null);

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
        
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<string>())).ReturnsAsync((MeetUp?)null);
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());

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
        meetUpRepositoryStub.Setup(x => x.CreateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<string>()))
                            .ReturnsAsync(new MeetUp());
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,AddMeetUpModel>(It.IsAny<AddMeetUpModel>()))
            .Returns(new MeetUp());
        
        var result = await controller.CreateMeetUp(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateMeetUp_WithModelNull_ReturnsBadRequest()
    {
        UpdateMeetUpModel model = null!;

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

        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync((Location?)null);

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
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());

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
        
        meetUpRepositoryStub.Setup(x => x.UpdateMeetUp(It.IsAny<MeetUp>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<MeetUp,UpdateMeetUpModel>(It.IsAny<UpdateMeetUpModel>()))
            .Returns(new MeetUp());

        var result = await controller.UpdateMeetUp(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetUp_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;

        var result = await controller.DeleteMeetUp(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetUp_WithUnexistMeetUp_ReturnsNotFound()
    {
        int id = 1;
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);

        var result = await controller.DeleteMeetUp(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetUp_WithValidModel_ReturnsOk()
    {
        int id = 1;
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());

        var result = await controller.DeleteMeetUp(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetMeetUpByLocation_WithInvalidLocation_ReturnsBadRequest()
    {
        int locationId = 0;

        var result = await controller.GetMeetUpByLocation(locationId);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetMeetUpByLocation_WithUnexistLocation_ReturnsBadRequest()
    {
        int locationId = 1;
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync((Location?)null);

        var result = await controller.GetMeetUpByLocation(locationId);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetMeetUpByLocation_WithValidLocation_ReturnsOk()
    {
        int locationId = 1;
        meetUpRepositoryStub.Setup(x => x.GetMeetUpsByLocation(It.IsAny<int>()))
                            .ReturnsAsync(new List<MeetUp>());
        
        locationRepositoryStub.Setup(x => x.GetLocation(It.IsAny<int>()))
                            .ReturnsAsync(new Location());
        
        var result = await controller.GetMeetUpByLocation(locationId);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithModelNull_ReturnsBadRequest()
    {
        AddEventModel model = null!;

        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithInvalidModel_ReturnsBadRequest()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = "Event Test",
            StartHour = "asdfa",
            MeetUpId = 1
        };

        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithInvalidName_ReturnsBadRequest()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = string.Empty,
            StartHour = "22:35",
            MeetUpId = 1
        };
        
        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithEmptyHour_ReturnsBadRequest()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = "Event test",
            StartHour = string.Empty,
            MeetUpId = 1
        };

        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithInvalidMeetUp_ReturnsBadRequest()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = "Event test",
            StartHour = "22:35",
            MeetUpId = 0
        };

        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithUnexistMeetUp_ReturnsBadRequest()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = "Event test",
            StartHour = "22:35",
            MeetUpId = 1
        };

        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);

        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithProblemsToAdd_ReturnsBadRequest()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = "Event test",
            StartHour = "22:35",
            MeetUpId = 1
        };
        
        meetUpRepositoryStub.Setup(x => x.CreateEvent(It.IsAny<Event>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<string>()))
                            .ReturnsAsync((Event?)null);
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Event,AddEventModel>(It.IsAny<AddEventModel>()))
            .Returns(new Event());

        var result = await controller.AddEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task AddEvent_WithValidModel_ReturnsOk()
    {
        AddEventModel model = new AddEventModel()
        {
            Name = "Event test",
            StartHour = "22:35",
            MeetUpId = 1
        };
        
        meetUpRepositoryStub.Setup(x => x.CreateEvent(It.IsAny<Event>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<string>()))
                            .ReturnsAsync(new Event());
        mappingHelperStub
            .Setup(conv => conv.ConvertTo<Event,AddEventModel>(It.IsAny<AddEventModel>()))
            .Returns(new Event());

        var result = await controller.AddEvent(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithInvalidModel_ReturnsBadRequest()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = "Event test",
            StartHour = "22:89",
            MeetUpId = 1
        };

        var result = await controller.UpdateEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithInvalidId_ReturnsBadRequest()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 0,
            Name = "Event test",
            StartHour = "22:30",
            MeetUpId = 1
        };

        var result = await controller.UpdateEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithInvalidName_ReturnsBadRequest()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = string.Empty,
            StartHour = "22:30",
            MeetUpId = 1
        };
        
        var result = await controller.UpdateEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithEmptyHour_ReturnsBadRequest()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = "Event test",
            StartHour = string.Empty,
            MeetUpId = 1
        };

        var result = await controller.UpdateEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithInvalidMeetUp_ReturnsBadRequest()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = "Event test",
            StartHour = "22:30",
            MeetUpId = 0
        };

        var result = await controller.UpdateEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithUnexistEvent_ReturnsNotFound()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = "Event test",
            StartHour = "22:30",
            MeetUpId = 1
        };

        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync((Event?)null);

        var result = await controller.UpdateEvent(model);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithUnexistMeetUp_ReturnsBadRequest()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = "Event test",
            StartHour = "22:30",
            MeetUpId = 1
        };
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync(new Event());

        var result = await controller.UpdateEvent(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdateEvent_WithValidModel_ReturnsOk()
    {
        UpdateEventModel model = new UpdateEventModel()
        {
            Id = 1,
            Name = "Event test",
            StartHour = "22:30",
            MeetUpId = 1
        };
        
        meetUpRepositoryStub.Setup(x => x.UpdateEvent(It.IsAny<Event>()));
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync(new Event());
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<string>()))
                            .ReturnsAsync(new Event());

        var result = await controller.UpdateEvent(model);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteEvent_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;

        var result = await controller.DeleteEvent(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task DeleteEvent_WithUnexistEvent_ReturnsNotFound()
    {
        int id = 1;

        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync((Event?)null);

        var result = await controller.DeleteEvent(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteEvent_WithValidModel_ReturnsOk()
    {
        int id = 1;

        meetUpRepositoryStub.Setup(x => x.DeleteEvent(It.IsAny<Event>()));
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync(new Event());

        var result = await controller.DeleteEvent(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetEvent_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;

        var result = await controller.GetEvent(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetEvent_WithUnexistEvent_ReturnsNotFound()
    {
        int id = 1;

        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync((Event?)null);
        
        var result = await controller.GetEvent(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetEvent_WithValidModel_ReturnsOk()
    {
        int id = 1;
        
        meetUpRepositoryStub.Setup(x => x.GetEvent(It.IsAny<int>()))
                            .ReturnsAsync(new Event());

        var result = await controller.GetEvent(id);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetEventsByMeetUp_WithInvalidId_ReturnsBadRequest()
    {
        int id = 0;

        var result = await controller.GetEventsByMeetUp(id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetEventsByMeetUp_WithUnexistMeetUp_ReturnsNotFound()
    {
        int id = 1;

        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync((MeetUp?)null);

        var result = await controller.GetEventsByMeetUp(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetEventsByMeetUp_WithValidModel_ReturnsOk()
    {
        int id = 1;
        
        meetUpRepositoryStub.Setup(x => x.GetEventsByMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new List<Event>());
        meetUpRepositoryStub.Setup(x => x.GetMeetUp(It.IsAny<int>()))
                            .ReturnsAsync(new MeetUp());

        var result = await controller.GetEventsByMeetUp(id);

        Assert.IsType<OkObjectResult>(result);
    }
}