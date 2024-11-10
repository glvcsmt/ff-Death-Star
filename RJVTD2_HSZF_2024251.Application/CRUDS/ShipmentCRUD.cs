using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql;

namespace RJVTD2_HSZF_2024251.Application.CRUDS;

public class ShipmentCRUD
{
    DeathStarDbContext Context { get; set; }
    public ShipmentCRUD(DeathStarDbContext context)
    {
        Context = context;
    }
    
    public void CreateShipment(Shipment shipment)
    {
        Context.Shipments.Add(shipment);
        Context.SaveChanges();
    }

    public IEnumerable<Shipment> ReadAllShipments()
    {
        return Context.Shipments;
    }

    public void UpdateShipment(Shipment shipment)
    {
        var shipmentToUpdate = Context.Shipments.FirstOrDefault(t => t.Id == shipment.Id);

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

        Context.SaveChanges();
    }

    public void DeleteShipment(string id)
    {
        var shipmentToDelete = Context.Shipments.FirstOrDefault(t => t.Id == id);
        if (shipmentToDelete != null) Context.Shipments.Remove(shipmentToDelete);
        
        Context.SaveChanges();
    }
}