using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface IXMLService
{
    void WriteShipmentReport(Shipment shipment);
}

public class XMLService : IXMLService
{
    private readonly IXMLProvider _xmlProvider;

    public XMLService(IXMLProvider xmlProvider)
    {
        _xmlProvider = xmlProvider;
    }
    
    public void WriteShipmentReport(Shipment shipment)
    {
         _xmlProvider.WriteShipmentReport(shipment);
    }
}