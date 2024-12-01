using NUnit.Framework;
using Moq;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Test
{
    [TestFixture]
    public class ShipmentServiceTests
    {
        private Mock<IShipmentDataProvider> _mockDataProvider;
        private ShipmentService _shipmentService;

        [SetUp]
        public void SetUp()
        {
            // Arrange: Create a mock for the data provider
            _mockDataProvider = new Mock<IShipmentDataProvider>();

            // Inject the mock into the service
            _shipmentService = new ShipmentService(_mockDataProvider.Object);
        }

        [Test]
        public void GetShipmentById_ShouldReturnShipment_WhenIdExists()
        {
            // Arrange
            var shipmentId = "S123";
            var expectedShipment = new Shipment
            {
                Id = shipmentId,
                ShipType = "Cargo Ship",
                ShipmentDate = DateTime.Now,
                Status = "In Transit",
                ImperialPermitNumber = "IP123456",
                CargoCapacity = new CargoCapacity("Tons", 500, shipmentId),
                Crew = new Crew("Captain John", 10, shipmentId)
            };

            // Mock the data provider to return the expected result
            _mockDataProvider.Setup(m => m.GetShipmentById(shipmentId))
                .Returns(expectedShipment);

            // Act
            var result = _shipmentService.GetShipmentById(shipmentId);

            // Assert
            Assert.That(result.Id, Is.EqualTo(expectedShipment.Id));
            Assert.That(result.ShipType, Is.EqualTo(expectedShipment.ShipType));
            Assert.That(result.Status, Is.EqualTo(expectedShipment.Status));
            Assert.That(result.ImperialPermitNumber, Is.EqualTo(expectedShipment.ImperialPermitNumber));
            Assert.That(result.CargoCapacity, Is.Not.Null);
            Assert.That(result.CargoCapacity.Unit, Is.EqualTo(expectedShipment.CargoCapacity.Unit));
            Assert.That(result.CargoCapacity.Amount, Is.EqualTo(expectedShipment.CargoCapacity.Amount));
            Assert.That(result.Crew, Is.Not.Null);
            Assert.That(result.Crew.CaptainName, Is.EqualTo(expectedShipment.Crew.CaptainName));
        }

        [Test]
        public void CreateShipment_ShouldCallCreateMethod_WhenShipmentIsProvided()
        {
            // Arrange
            var newShipment = new Shipment
            {
                Id = "S124",
                ShipType = "Oil Tanker",
                ShipmentDate = DateTime.Now,
                Status = "Scheduled",
                ImperialPermitNumber = "IP654321",
                CargoCapacity = new CargoCapacity("Barrels", 200, "S124"),
                Crew = new Crew("Captain Smith", 20, "S124")
            };

            // Act
            _shipmentService.CreateShipment(newShipment);

            // Assert
            _mockDataProvider.Verify(m => m.CreateShipment(newShipment), Times.Once);
        }

        [Test]
        public void ReadAllShipments_ShouldReturnAllShipments()
        {
            // Arrange
            var shipmentList = new List<Shipment>
            {
                new Shipment
                {
                    Id = "S125",
                    ShipType = "Container Ship",
                    ShipmentDate = DateTime.Now,
                    Status = "Docked",
                    ImperialPermitNumber = "IP789123",
                    CargoCapacity = new CargoCapacity("Containers", 150, "S125"),
                    Crew = new Crew("Captain Alice", 15, "S125")
                },
                new Shipment
                {
                    Id = "S126",
                    ShipType = "Bulk Carrier",
                    ShipmentDate = DateTime.Now,
                    Status = "In Transit",
                    ImperialPermitNumber = "IP987654",
                    CargoCapacity = new CargoCapacity("Tons", 1000, "S126"),
                    Crew = new Crew("Captain Bob", 25, "S126")
                }
            };

            // Mock the data provider to return the shipment list
            _mockDataProvider.Setup(m => m.ReadAllShipments())
                .Returns(shipmentList);

            // Act
            var result = _shipmentService.ReadAllShipments().ToList();

            // Assert
            Assert.That(2, Is.EqualTo(result.Count));
            Assert.That(result[0].Id, Is.EqualTo(shipmentList[0].Id));
            Assert.That(result[1].Id, Is.EqualTo(shipmentList[1].Id));
        }

        [Test]
        public void UpdateShipment_ShouldCallUpdateMethod_WhenShipmentIsProvided()
        {
            // Arrange
            var updatedShipment = new Shipment
            {
                Id = "S127",
                ShipType = "Passenger Ship",
                ShipmentDate = DateTime.Now,
                Status = "Completed",
                ImperialPermitNumber = "IP321654",
                CargoCapacity = new CargoCapacity("Passengers", 500, "S127"),
                Crew = new Crew("Captain Charlie", 30, "S127")
            };

            // Act
            _shipmentService.UpdateShipment(updatedShipment);

            // Assert
            _mockDataProvider.Verify(m => m.UpdateShipment(updatedShipment), Times.Once);
        }

        [Test]
        public void DeleteShipment_ShouldCallDeleteMethod_WhenIdIsProvided()
        {
            // Arrange
            var shipmentId = "S128";

            // Act
            _shipmentService.DeleteShipment(shipmentId);

            // Assert
            _mockDataProvider.Verify(m => m.DeleteShipment(shipmentId), Times.Once);
        }
    }
}
