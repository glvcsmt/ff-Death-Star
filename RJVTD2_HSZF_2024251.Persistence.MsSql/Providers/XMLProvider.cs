using System.Xml.Linq;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

public interface IXMLProvider
{
    void WriteShipmentReport(Shipment shipment);
}

public class XMLProvider : IXMLProvider
{
    private double totalCargoWorth;

    public void WriteShipmentReport(Shipment shipment)
    {
        foreach (Cargo cargo in shipment.Cargoes)
        {
            totalCargoWorth += cargo.ImperialCredits;
        }
        
        XElement root = new XElement("ShipmentReport",
            new XElement("ShippingDate", shipment.ShipmentDate.Value.ToString("yyyy-MM-dd")),
            new XElement("CargoesCount", shipment.Cargoes.Count),
            new XElement("TotalCargoWorth", totalCargoWorth)); 
        
        string fileName = $"ShipmentReport_{shipment.ShipmentDate.Value.ToString("yyyyMMMMdd")}.xml";
        string directoryBasePath = "../../../../Shipments";
        string filePath = Path.Combine(directoryBasePath, shipment.ImperialPermitNumber ,fileName);
        
        XDocument xmlDocument = new XDocument(root);
        xmlDocument.Save(filePath);
    }
}