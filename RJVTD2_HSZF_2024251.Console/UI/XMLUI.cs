using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

// The XMLUI class manages the creation of XML reports related to shipments.
// It allows users to generate a report for a specific shipment based on its ID.
public class XMLUI
{
    // Private fields to store service dependencies for shipment and XML report generation.
    IShipmentService _shipmentService;
    IXMLService _xmlService;
    
    // Event to notify when an XML report has been created successfully.
    public event Action<string> XMLReportCreated;

    // Constructor to initialize the XMLUI with its dependencies via dependency injection.
    // This ensures that the necessary services for generating XML reports and handling shipments are available.
    public XMLUI(IXMLService xmlService, IShipmentService shipmentService)
    {
        _xmlService = xmlService;
        _shipmentService = shipmentService;
    }

    // Prompts the user for a shipment ID, retrieves the shipment by that ID, and generates an XML report.
    // Notifies users once the report has been successfully created for the specified shipment.
    public void CreateXMLReport()
    {
        try
        {
            // Prompt for the shipment ID and fetch the shipment details.
            string shipmentForReportID = Commands.GetString("Enter the ID of the shipment you want to create a report of: ");
            Shipment shipmentToReport = _shipmentService.GetShipmentById(shipmentForReportID);
                
            // Call the XML service to generate the report based on the retrieved shipment.
            _xmlService.WriteShipmentReport(shipmentToReport);
                
            // Notify that the XML report was created successfully.
            XMLReportCreated?.Invoke($"The report for the shipment with the ID {shipmentForReportID} was created.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the process of report creation.
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
}