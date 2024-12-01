using NUnit.Framework;
using Moq;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Test;

[TestFixture]
public class XMLServiceTests
{
    private Mock<IXMLProvider> _mockXmlProvider;
    private XMLService _xmlService;

    [SetUp]
    public void SetUp()
    {
        _mockXmlProvider = new Mock<IXMLProvider>();
        _xmlService = new XMLService(_mockXmlProvider.Object);
    }

    [Test]
    public void WriteShipmentReport_ShouldCallWriteShipmentReport_WhenShipmentIsProvided()
    {
        var shipment = new Shipment
        {
            Id = "S123",
            ShipType = "Cargo Ship",
            ShipmentDate = DateTime.Now,
            Status = "In Transit",
            ImperialPermitNumber = "IP12345",
            CargoCapacity = new CargoCapacity { Amount = 1000 },
            Crew = new Crew { CaptainName = "Captain A", CrewCount = 10 }
        };

        _xmlService.WriteShipmentReport(shipment);

        _mockXmlProvider.Verify(m => m.WriteShipmentReport(shipment), Times.Once);
    }
}