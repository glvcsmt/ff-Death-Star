using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface IShipmentService
{
    Shipment GetShipmentById(int shipmentId);
    
    void CreateShipment(Shipment shipment);
    
    IEnumerable<Shipment> ReadAllShipments();
    
    void UpdateCargo(Shipment shipment);
    
    void DeleteShipment(int shipmentId);
}

public class ShipmentService : IShipmentService
{
    private readonly IShipmentDataProvider _shipmentDataProvider;

    public ShipmentService(IShipmentDataProvider shipmentDataProvider)
    {
        this._shipmentDataProvider = shipmentDataProvider;
    }

    public Shipment GetShipmentById(int shipmentId)
    {
        return _shipmentDataProvider.GetShipmentById(shipmentId);
    }

    public void CreateShipment(Shipment shipment)
    {
        _shipmentDataProvider.CreateShipment(shipment);
    }

    public IEnumerable<Shipment> ReadAllShipments()
    {
        return _shipmentDataProvider.ReadAllShipments();
    }

    public void UpdateCargo(Shipment shipment)
    {
        _shipmentDataProvider.UpdateCargo(shipment);
    }

    public void DeleteShipment(int shipmentId)
    {
        _shipmentDataProvider.DeleteShipment(shipmentId);
    }
}