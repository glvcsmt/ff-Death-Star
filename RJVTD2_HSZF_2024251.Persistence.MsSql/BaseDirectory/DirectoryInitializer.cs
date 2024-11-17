using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.BaseDirectory;

public class DirectoryInitializer
{
    private string baseDirPath = "../../../../Shipments";
    
    ShipmentDataProvider _shipmentDataProvider;
    
    public DirectoryInitializer(DeathStarDbContext context)
    {
        this._shipmentDataProvider = new ShipmentDataProvider(context);
        BaseDirInitializer();
    }
    
    private void BaseDirInitializer()
    {
        Directory.CreateDirectory(baseDirPath);

        List<Shipment> shipments = _shipmentDataProvider.ReadAllShipments().ToList();

        foreach (Shipment ship in shipments)
        {
            Directory.CreateDirectory(Path.Combine(baseDirPath, ship.ImperialPermitNumber));
        }
    }
}