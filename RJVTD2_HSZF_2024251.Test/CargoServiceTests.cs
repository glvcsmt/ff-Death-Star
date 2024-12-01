using NUnit.Framework;
using Moq;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Test;

[TestFixture]
public class CargoServiceTests
{
    private Mock<ICargoDataProvider> _mockDataProvider;
    private CargoService _cargoService;

    [SetUp]
    public void SetUp()
    {
        _mockDataProvider = new Mock<ICargoDataProvider>();
        
        _cargoService = new CargoService(_mockDataProvider.Object);
    }

    [Test]
    public void GetCargoById_ShouldReturnCargo_WhenIdExists()
    {
        var cargoId = "C123";
        var expectedCargo = new Cargo
        {
            Id = cargoId,
            CargoType = "Electronics",  
            Quantity = 100,
            ImperialCredits = 5000,
            Insurance = true,
            RiskLevel = RiskLevel.Medium,
            ShipmentId = "S123",
            Shipment = new Shipment { Id = "S123", ShipType = "Shipment123" }
        };
        
        _mockDataProvider.Setup(m => m.GetCargoById(cargoId))
            .Returns(expectedCargo);
        
        var result = _cargoService.GetCargoById(cargoId);
        
        Assert.That(result.Id, Is.EqualTo(expectedCargo.Id));
        Assert.That(result.CargoType, Is.EqualTo(expectedCargo.CargoType));
        Assert.That(result.Quantity, Is.EqualTo(expectedCargo.Quantity));
        Assert.That(result.ImperialCredits, Is.EqualTo(expectedCargo.ImperialCredits));
        Assert.That(result.Insurance, Is.EqualTo(expectedCargo.Insurance));
        Assert.That(result.RiskLevel, Is.EqualTo(expectedCargo.RiskLevel));
        Assert.That(result.ShipmentId, Is.EqualTo(expectedCargo.ShipmentId));
        Assert.That(result.Shipment, Is.Not.Null);
        Assert.That(result.Shipment.Id, Is.EqualTo(expectedCargo.Shipment.Id));
        Assert.That(result.Shipment.ShipType, Is.EqualTo(expectedCargo.Shipment.ShipType));
    }

    [Test]
    public void CreateCargo_ShouldCallCreateMethod_WhenCargoIsProvided()
    {
        var newCargo = new Cargo
        {
            Id = "C124",
            CargoType = "Food",  
            Quantity = 50,
            ImperialCredits = 2000,
            Insurance = false,
            RiskLevel = RiskLevel.Low,
            ShipmentId = "S124",
            Shipment = new Shipment { Id = "S124", ShipType = "Shipment124" }
        };
        
        _cargoService.CreateCargo(newCargo);
        
        _mockDataProvider.Verify(m => m.CreateCargo(newCargo), Times.Once);
    }

    [Test]
    public void ReadAllCargoes_ShouldReturnAllCargoes()
    {
        var cargoList = new List<Cargo>
        {
            new Cargo { Id = "C125", CargoType = "Clothing", Quantity = 200, ImperialCredits = 1000, Insurance = true, RiskLevel = RiskLevel.Low, ShipmentId = "S125", Shipment = new Shipment { Id = "S125", ShipType = "Shipment125" } },
            new Cargo { Id = "C126", CargoType = "Medicines", Quantity = 30, ImperialCredits = 7000, Insurance = true, RiskLevel = RiskLevel.Critical, ShipmentId = "S126", Shipment = new Shipment { Id = "S126", ShipType = "Shipment126" } }
        };
        
        _mockDataProvider.Setup(m => m.ReadAllCargoes())
            .Returns(cargoList);
        
        var result = _cargoService.ReadAllCargoes().ToList();
        
        Assert.That(2, Is.EqualTo(result.Count));
        Assert.That(result[0].Id, Is.EqualTo(cargoList[0].Id));
        Assert.That(result[1].Id, Is.EqualTo(cargoList[1].Id));
    }

    [Test]
    public void UpdateCargo_ShouldCallUpdateMethod_WhenCargoIsProvided()
    {
        var updatedCargo = new Cargo
        {
            Id = "C127",
            CargoType = "Furniture",  
            Quantity = 15,
            ImperialCredits = 3500,
            Insurance = true,
            RiskLevel = RiskLevel.Medium,
            ShipmentId = "S127",
            Shipment = new Shipment { Id = "S127", ShipType = "Shipment127" }
        };
        
        _cargoService.UpdateCargo(updatedCargo);
        
        _mockDataProvider.Verify(m => m.UpdateCargo(updatedCargo), Times.Once);
    }

    [Test]
    public void DeleteCargo_ShouldCallDeleteMethod_WhenIdIsProvided()
    {
        var cargoId = "C128";
        
        _cargoService.DeleteCargo(cargoId);
        
        _mockDataProvider.Verify(m => m.DeleteCargo(cargoId), Times.Once);
    }
}