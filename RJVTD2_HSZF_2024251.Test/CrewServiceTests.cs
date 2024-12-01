using NUnit.Framework;
using Moq;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Test;

[TestFixture]
public class CrewServiceTests
{
    private Mock<ICrewDataProvider> _mockDataProvider;
    private CrewService _crewService;

    [SetUp]
    public void SetUp()
    {
        _mockDataProvider = new Mock<ICrewDataProvider>();
        
        _crewService = new CrewService(_mockDataProvider.Object);
    }

    [Test]
    public void GetCrewById_ShouldReturnCrew_WhenIdExists()
    {
        var crewId = "C123";
        var expectedCrew = new Crew
        {
            Id = crewId,
            CaptainName = "Captain A",
            CrewCount = 10,
            ShipmentId = "S123",
            Shipment = new Shipment { Id = "S123", ShipType = "Shipment123" }
        };
        
        _mockDataProvider.Setup(m => m.GetCrewById(crewId))
            .Returns(expectedCrew);
        
        var result = _crewService.GetCrewById(crewId);
        
        Assert.That(expectedCrew.Id, Is.EqualTo(result.Id));
        Assert.That(expectedCrew.CaptainName, Is.EqualTo(result.CaptainName));
        Assert.That(expectedCrew.CrewCount, Is.EqualTo(result.CrewCount));
        Assert.That(expectedCrew.ShipmentId, Is.EqualTo(result.ShipmentId));
        Assert.That(expectedCrew.Shipment, Is.Not.Null);
        Assert.That(expectedCrew.Shipment.Id, Is.EqualTo(result.Shipment.Id));
        Assert.That(expectedCrew.Shipment.ShipType, Is.EqualTo(result.Shipment.ShipType));
    }

    [Test]
    public void CreateCrew_ShouldCallCreateMethod_WhenCrewIsProvided()
    {
        var newCrew = new Crew
        {
            Id = "C124",
            CaptainName = "Captain B",
            CrewCount = 12,
            ShipmentId = "S124",
            Shipment = new Shipment { Id = "S124", ShipType = "Shipment124" }
        };
        
        _crewService.CreateCrew(newCrew);
        
        _mockDataProvider.Verify(m => m.CreateCrew(newCrew), Times.Once);
    }

    [Test]
    public void ReadAllCrews_ShouldReturnAllCrews()
    {
        var crewList = new List<Crew>
        {
            new Crew { Id = "C125", CaptainName = "Captain C", CrewCount = 15, ShipmentId = "S125", Shipment = new Shipment { Id = "S125", ShipType = "Shipment125" } },
            new Crew { Id = "C126", CaptainName = "Captain D", CrewCount = 8, ShipmentId = "S126", Shipment = new Shipment { Id = "S126", ShipType = "Shipment126" } }
        };
        
        _mockDataProvider.Setup(m => m.ReadAllCrews())
            .Returns(crewList);
        
        var result = _crewService.ReadAllCrews().ToList();
        
        Assert.That(2, Is.EqualTo(result.Count()));
        Assert.That(result[0], Is.EqualTo(crewList[0]));
        Assert.That(result[1], Is.EqualTo(crewList[1]));
    }

    [Test]
    public void UpdateCrew_ShouldCallUpdateMethod_WhenCrewIsProvided()
    {
        var updatedCrew = new Crew
        {
            Id = "C127",
            CaptainName = "Captain E",
            CrewCount = 20,
            ShipmentId = "S127",
            Shipment = new Shipment { Id = "S127", ShipType = "Shipment127" }
        };
        
        _crewService.UpdateCrew(updatedCrew);
        
        _mockDataProvider.Verify(m => m.UpdateCrew(updatedCrew), Times.Once);
    }

    [Test]
    public void DeleteCrew_ShouldCallDeleteMethod_WhenIdIsProvided()
    {
        var crewId = "C128";
        
        _crewService.DeleteCrew(crewId);
        
        _mockDataProvider.Verify(m => m.DeleteCrew(crewId), Times.Once);
    }
}
