using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

public interface IShipmentDataProvider
{
    Shipment GetShipmentById(int shipmentId);
    
    void CreateShipment(Shipment shipment);
    
    IEnumerable<Shipment> ReadAllShipments();
    
    void UpdateCargo(Shipment shipment);
    
    void DeleteShipment(int shipmentId);
}

public class ShipmentDataProvider : IShipmentDataProvider
{
    private readonly DeathStarDbContext _context;

    public ShipmentDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    public Shipment GetShipmentById(int shipmentId)
    {
        return _context.Shipments.FirstOrDefault(t => t.Id.Equals(shipmentId));
    }

    public void CreateShipment(Shipment shipment)
    {
        _context.Shipments.Add(shipment);
        _context.SaveChanges();
    }

    public IEnumerable<Shipment> ReadAllShipments()
    {
        return _context.Shipments;
    }

    public void UpdateCargo(Shipment shipment)
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

    public void DeleteShipment(int shipmentId)
    {
        Shipment shipmentToDelete = _context.Shipments.FirstOrDefault(t => t.Id.Equals(shipmentId));
        if (shipmentToDelete != null) _context.Shipments.Remove(shipmentToDelete);
        
        _context.SaveChanges();
    }
}