using Moq;
using NUnit.Framework;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Test;

    [TestFixture]
    public class CargoCapacityServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _mockDataProvider = new Mock<ICargoCapacityDataProvider>();
            
            _cargoCapacityService = new CargoCapacityService(_mockDataProvider.Object);
        }

        private Mock<ICargoCapacityDataProvider> _mockDataProvider;
        private CargoCapacityService _cargoCapacityService;

        [Test]
        public void GetCargoCapacityById_ShouldReturnCargoCapacity_WhenIdExists()
        {
            var cargoCapacityId = "123";
            var expectedShipment = new Shipment { Id = "S123", ShipType = "Shipment123" };
            var expectedCargoCapacity = new CargoCapacity
            {
                Id = cargoCapacityId,
                Unit = "kg",
                Amount = 100,
                ShipmentId = "S123",
                Shipment = expectedShipment
            };
            
            _mockDataProvider.Setup(m => m.GetCargoCapacityById(cargoCapacityId))
                .Returns(expectedCargoCapacity);
            
            var result = _cargoCapacityService.GetCargoCapacityById(cargoCapacityId);
            
            Assert.That(result, Is.EqualTo(expectedCargoCapacity));
            Assert.That(result.Shipment, Is.EqualTo(expectedShipment));
        }

        [Test]
        public void CreateCargoCapacity_ShouldCallCreateMethod_WhenCargoCapacityIsProvided()
        {
            var newCargoCapacity = new CargoCapacity
            {
                Id = "124",
                Unit = "kg",
                Amount = 200,
                ShipmentId = "S124",
                Shipment = new Shipment { Id = "S124", ShipType = "Shipment124" }
            };
            
            _cargoCapacityService.CreateCargoCapacity(newCargoCapacity);
            
            _mockDataProvider.Verify(m => m.CreateCargoCapacity(newCargoCapacity), Times.Once);
        }

        [Test]
        public void ReadAllCargoCapacities_ShouldReturnAllCargoCapacities()
        {
            var cargoCapacities = new List<CargoCapacity>
            {
                new CargoCapacity { Id = "125", Unit = "kg", Amount = 300, ShipmentId = "S125", Shipment = new Shipment { Id = "S125", ShipType = "Shipment125" } },
                new CargoCapacity { Id = "126", Unit = "kg", Amount = 400, ShipmentId = "S126", Shipment = new Shipment { Id = "S126", ShipType = "Shipment126" } }
            };
            
            _mockDataProvider.Setup(m => m.ReadAllCargoCapacities())
                .Returns(cargoCapacities);
            
            var result = _cargoCapacityService.ReadAllCargoCapacities();
            
            Assert.That(result, Is.EqualTo(cargoCapacities));
        }

        [Test]
        public void UpdateCargoCapacity_ShouldCallUpdateMethod_WhenCargoCapacityIsProvided()
        {
            var updatedCargoCapacity = new CargoCapacity
            {
                Id = "127",
                Unit = "kg",
                Amount = 500,
                ShipmentId = "S127",
                Shipment = new Shipment { Id = "S127", ShipType = "Shipment127" }
            };
            
            _cargoCapacityService.UpdateCargoCapacity(updatedCargoCapacity);
            
            _mockDataProvider.Verify(m => m.UpdateCargoCapacity(updatedCargoCapacity), Times.Once);
        }

        [Test]
        public void DeleteCargoCapacity_ShouldCallDeleteMethod_WhenIdIsProvided()
        {
            var cargoCapacityId = "128";
            
            _cargoCapacityService.DeleteCargoCapacity(cargoCapacityId);
            
            _mockDataProvider.Verify(m => m.DeleteCargoCapacity(cargoCapacityId), Times.Once);
        }
    }