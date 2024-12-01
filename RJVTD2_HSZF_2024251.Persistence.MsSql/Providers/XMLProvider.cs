using System.Xml.Linq;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

// Interface defining the method for writing a shipment report to an XML file
public interface IXMLProvider
{
    // Writes a shipment report based on the provided shipment
    void WriteShipmentReport(Shipment shipment);
}

// Implementation of the IXMLProvider interface
// This class generates an XML file representing a shipment report
public class XMLProvider : IXMLProvider
{
    // Variable to hold the total value of cargo in the shipment
    private double totalCargoWorth;

    // Writes a shipment report to an XML file
    // This method calculates the total worth of cargo in the shipment and generates an XML document
    public void WriteShipmentReport(Shipment shipment)
    {
        // Loop through each cargo in the shipment to calculate the total worth
        foreach (Cargo cargo in shipment.Cargoes)
        {
            totalCargoWorth += cargo.ImperialCredits;
        }
        
        // Create the root element of the XML document
        XElement root = new XElement("ShipmentReport",
            new XElement("ShippingDate", shipment.ShipmentDate.Value.ToString("yyyy-MM-dd")),
            new XElement("CargoesCount", shipment.Cargoes.Count),
            new XElement("TotalCargoWorth", totalCargoWorth)); 
        
        // Generate the XML file name based on the shipment date
        string fileName = $"ShipmentReport_{shipment.ShipmentDate.Value.ToString("yyyyMMMMdd")}.xml";
        // Define the base path where the file will be saved
        string directoryBasePath = "../../../../Shipments";
        // Combine the directory path, shipment permit number, and file name to form the complete file path
        string filePath = Path.Combine(directoryBasePath, shipment.ImperialPermitNumber ,fileName);
        
        // Create a new XML document with the generated root element
        XDocument xmlDocument = new XDocument(root);
        // Save the XML document to the specified file path
        xmlDocument.Save(filePath);
    }
}