using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.BaseDirectory;

public class DirectoryInitializer
{
    ShipmentDataProvider _shipmentDataProvider;
    
    public DirectoryInitializer(DeathStarDbContext context)
    {
        _shipmentDataProvider = new ShipmentDataProvider(context);
        BaseDirInitializer();
    }
    
    private void BaseDirInitializer()
    {
        DirectoryProvider directoryProvider = new DirectoryProvider();

        List<Shipment> shipments = _shipmentDataProvider.ReadAllShipments().ToList();

        foreach (Shipment shipment in shipments)
        {
            if (shipment.ImperialPermitNumber != null) directoryProvider.CreateDirectory(shipment.ImperialPermitNumber);
        }
    }
}