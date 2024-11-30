using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class XMLUI
{
    IShipmentService _shipmentService;
    IXMLService _xmlService;
    
    public event Action<string> XMLReportCreated;

    public XMLUI(IXMLService xmlService, IShipmentService shipmentService)
    {
        _xmlService = xmlService;
        _shipmentService = shipmentService;
    }

    public void CreateXMLReport()
    {
        string shipmentForReportID = Commands.GetString("Enter the ID of the shipment you want to create a report of: ");
        Shipment shipmentToReport = _shipmentService.GetShipmentById(shipmentForReportID);
        ;
        _xmlService.WriteShipmentReport(shipmentToReport);
        
        XMLReportCreated?.Invoke($"The report for the shipment with the ID {shipmentForReportID} was created.");
    }
}