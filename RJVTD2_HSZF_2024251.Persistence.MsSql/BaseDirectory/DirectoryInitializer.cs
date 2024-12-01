using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.BaseDirectory;

// Class responsible for initializing directories for shipments based on their Imperial Permit Numbers
public class DirectoryInitializer
{
    // Private field to hold an instance of the ShipmentDataProvider, which is used to access shipment data
    ShipmentDataProvider _shipmentDataProvider;
    
    // Constructor that accepts a DeathStarDbContext instance and initializes the DirectoryInitializer
    public DirectoryInitializer(DeathStarDbContext context)
    {
        // Initializes the ShipmentDataProvider with the provided context to interact with the database
        _shipmentDataProvider = new ShipmentDataProvider(context);
        // Calls the BaseDirInitializer method to initialize directories for all shipments
        BaseDirInitializer();
    }
    
    // Method that initializes directories based on the Imperial Permit Numbers of shipments
    private void BaseDirInitializer()
    {
        // Create an instance of DirectoryProvider to handle directory operations
        DirectoryProvider directoryProvider = new DirectoryProvider();

        // Retrieve all shipments from the database using the ShipmentDataProvider
        List<Shipment> shipments = _shipmentDataProvider.ReadAllShipments().ToList();

        // Iterate through each shipment to check if it has a valid Imperial Permit Number
        foreach (Shipment shipment in shipments)
        {
            // If the shipment has a valid Imperial Permit Number, create a directory for it
            // Call CreateDirectory method of DirectoryProvider to create the directory
            if (shipment.ImperialPermitNumber != null) directoryProvider.CreateDirectory(shipment.ImperialPermitNumber);
        }
    }
}