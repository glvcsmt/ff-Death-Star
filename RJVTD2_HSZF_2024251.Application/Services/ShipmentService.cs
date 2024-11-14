using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface defining operations for Shipment service layer
public interface IShipmentService
{
    // Retrieves a Shipment entity by its ID
    Shipment GetShipmentById(int shipmentId);
    
    // Creates a new Shipment entity
    void CreateShipment(Shipment shipment);
    
    // Returns all Shipment entities
    IEnumerable<Shipment> ReadAllShipments();
    
    // Updates an existing Shipment entity
    void UpdateCargo(Shipment shipment);
    
    // Deletes a Shipment entity by its ID
    void DeleteShipment(int shipmentId);
}

// Implementation of the IShipmentService interface
// This service interacts with the IShipmentDataProvider for data operations
public class ShipmentService : IShipmentService
{
    private readonly IShipmentDataProvider _shipmentDataProvider;

    // Constructor accepting an IShipmentDataProvider instance for dependency injection
    public ShipmentService(IShipmentDataProvider shipmentDataProvider)
    {
        this._shipmentDataProvider = shipmentDataProvider;
    }

    // Retrieves a Shipment entity by its ID
    public Shipment GetShipmentById(int shipmentId)
    {
        return _shipmentDataProvider.GetShipmentById(shipmentId);
    }

    // Creates a new Shipment entity
    public void CreateShipment(Shipment shipment)
    {
        _shipmentDataProvider.CreateShipment(shipment);
    }

    // Retrieves and returns all Shipment entities
    public IEnumerable<Shipment> ReadAllShipments()
    {
        return _shipmentDataProvider.ReadAllShipments();
    }
    
    // Updates an existing Shipment entity
    public void UpdateCargo(Shipment shipment)
    {
        _shipmentDataProvider.UpdateCargo(shipment);
    }

    // Deletes a Shipment entity by its ID
    public void DeleteShipment(int shipmentId)
    {
        _shipmentDataProvider.DeleteShipment(shipmentId);
    }
}