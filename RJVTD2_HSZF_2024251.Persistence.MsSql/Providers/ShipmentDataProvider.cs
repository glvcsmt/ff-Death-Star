using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;


// Interface defining operations for managing Shipment entities in the database
public interface IShipmentDataProvider
{
    // Retrieves a Shipment entity by its ID
    Shipment GetShipmentById(string shipmentId);
    
    // Creates a new Shipment entity in the database
    void CreateShipment(Shipment shipment);
    
    // Returns all Shipment entities from the database
    IEnumerable<Shipment> ReadAllShipments();
    
    // Updates an existing Shipment entity in the database
    void UpdateShipment(Shipment shipment);
    
    // Deletes a Shipment entity by its ID
    void DeleteShipment(string shipmentId);
}

// Class implementation for the IShipmentDataProvider interface
// Uses DeathStarDbContext for database operations on Shipment entities
public class ShipmentDataProvider : IShipmentDataProvider
{
    private readonly DeathStarDbContext _context;

    // Constructor that accepts a DbContext for dependency injection
    public ShipmentDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    // Retrieves a Shipment entity by its ID
    public Shipment GetShipmentById(string shipmentId)
    {
        return _context.Shipments.FirstOrDefault(t => t.Id.Equals(shipmentId));
    }

    // Creates and saves a new Shipment entity in the database
    public void CreateShipment(Shipment shipment)
    {
        _context.Shipments.Add(shipment);
        _context.SaveChanges();
    }

    // Retrieves and returns all Shipment entities
    public IEnumerable<Shipment> ReadAllShipments()
    {
        return _context.Shipments;
    }

    // Updates an existing Shipment entity's properties in the database
    public void UpdateShipment(Shipment shipment)
    {
        
        Shipment shipmentToUpdate = _context.Shipments.FirstOrDefault(t => t.Id == shipment.Id);

        if (shipmentToUpdate != null)
        {
            shipmentToUpdate.ShipmentDate = shipment.ShipmentDate;
            shipmentToUpdate.CargoCapacity = shipment.CargoCapacity;
            shipmentToUpdate.Cargoes = shipment.Cargoes;
            shipmentToUpdate.Crew = shipment.Crew;
            shipmentToUpdate.Status = shipment.Status;
            shipmentToUpdate.ShipType = shipment.ShipType;
            shipmentToUpdate.ImperialPermitNumber = shipment.ImperialPermitNumber;
        }

        _context.SaveChanges();
    }

    // Deletes a Shipment entity by its ID, if it exists in the database
    public void DeleteShipment(string shipmentId)
    {
        Shipment shipmentToDelete = _context.Shipments.FirstOrDefault(t => t.Id.Equals(shipmentId));
        if (shipmentToDelete != null) _context.Shipments.Remove(shipmentToDelete);
        
        _context.SaveChanges();
    }
}