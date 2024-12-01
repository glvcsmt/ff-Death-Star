using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface that defines the operation for writing a shipment report in XML format
public interface IXMLService
{
    // Method signature for writing a shipment report
    void WriteShipmentReport(Shipment shipment);
}

// Implementation of the IXMLService interface
// This service interacts with the IXMLProvider to perform the XML report writing
public class XMLService : IXMLService
{
    // Private field that holds an instance of IXMLProvider for dependency injection
    private readonly IXMLProvider _xmlProvider;

    // Constructor that accepts an IXMLProvider instance for injecting dependencies
    public XMLService(IXMLProvider xmlProvider)
    {
        // Assign the injected IXMLProvider instance to the private field
        _xmlProvider = xmlProvider;
    }
    
    // Method that writes the shipment report by delegating to the IXMLProvider
    public void WriteShipmentReport(Shipment shipment)
    {
        // Delegate the responsibility of writing the shipment report to the IXMLProvider
         _xmlProvider.WriteShipmentReport(shipment);
    }
}