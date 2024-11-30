using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class ShipmentUI
{
    private IShipmentService _shipmentService;
    private CrewUI _crewUI;
    private CargoCapacityUI _cargoCapacityUI;
    private IDirectoryService _directoryService;
    
    public event Action<string> ShipmentCreated;
    public event Action<string> ShipmentUpdated;
    public event Action<string> ShipmentDeleted;
    public event Action<string> ShipmentLate;

    public ShipmentUI(IShipmentService shipmentService, ICrewService crewService, ICargoCapacityService cargoCapacityService, IDirectoryService directoryService)
    {
        _shipmentService = shipmentService;
        _crewUI = new CrewUI(crewService);
        _cargoCapacityUI = new CargoCapacityUI(cargoCapacityService);
        _directoryService = directoryService;
    }

    public void GetShipmentById()
    {
        string id = Commands.GetString("Enter the ID of the shipment you want to get: ");
        Shipment shipmentById = _shipmentService.GetShipmentById(id);
        
        System.Console.WriteLine($"Information about the shipment that has the ID: {id}:" +
                                 $"\n\t-Type of the ship: {shipmentById.ShipType}" +
                                 $"\n\t-Date of the shipping: {shipmentById.ShipmentDate}" +
                                 $"\n\t-Current status of the ship: {shipmentById.Status}" +
                                 $"\n\t-Imperial Permit Number of the ship: {shipmentById.ImperialPermitNumber}");
        _cargoCapacityUI.GetCargoCapacityById(id);
        _crewUI.GetCrewById(id);
    }
    
    public IEnumerable<Shipment> ReadAllShipments()
    {
        return _shipmentService.ReadAllShipments();
    }

    public void CreateShipment()
    {
        Shipment shipmentToCreate = new Shipment();
        shipmentToCreate.Id = Commands.GetString("Enter the ID of the shipment you want to create: ");
        shipmentToCreate.ShipType = Commands.GetString("Enter the ship type: ");
        shipmentToCreate.Status = Commands.GetString("Enter the ship's status: ");
        shipmentToCreate.ShipmentDate = DateTime.Parse(Commands.GetString("Enter the shipping date (yyyy-mm-dd): "));
        shipmentToCreate.ImperialPermitNumber = Commands.GetString("Enter the Imperial Permit Number of the shipment: ");
        _shipmentService.CreateShipment(shipmentToCreate);
        
        _cargoCapacityUI.CreateCargoCapacity(shipmentToCreate.Id);
        _crewUI.CreateCrew(shipmentToCreate.Id);
        
        ShipmentCreated?.Invoke($"Shipment with ID {shipmentToCreate.Id} has been created.");
        
        if (shipmentToCreate.ShipmentDate.Value.Date < DateTime.Now.Date)
        {
            string updatedStatus = shipmentToCreate.Status += ", Shipment is late!";
            shipmentToCreate.Status = updatedStatus;
            _shipmentService.UpdateShipment(shipmentToCreate);
            ShipmentLate?.Invoke($"Shipment with ID {shipmentToCreate.Id} is late, status modified!");
        }

        _directoryService.CreateDirectory(shipmentToCreate.ImperialPermitNumber);
    }

    public void UpdateShipment()
    {
        Shipment shipmentToUpdate = new Shipment();
        shipmentToUpdate.Id = Commands.GetString("Enter the ID of the shipment you want to update: ");
        shipmentToUpdate.ShipType = Commands.GetString("Enter the Ship Type: ");
        shipmentToUpdate.ShipmentDate = DateTime.Parse(Commands.GetString("Enter the shipping date (yyyy-mm-dd): "));
        _shipmentService.UpdateShipment(shipmentToUpdate);
        
        _cargoCapacityUI.UpdateCargoCapacity(shipmentToUpdate.Id);
        _crewUI.UpdateCrew(shipmentToUpdate.Id);
        
        ShipmentUpdated?.Invoke($"Shipment with ID {shipmentToUpdate.Id} has been updated.");
    }

    public void DeleteShipment()
    {
        string id = Commands.GetString("Enter the ID of the shipment you want to delete: ");
        
        Shipment directoryToDelete = _shipmentService.GetShipmentById(id);
        _directoryService.DeleteDirectory(directoryToDelete.ImperialPermitNumber);
        
        _shipmentService.DeleteShipment(id);
        _cargoCapacityUI.DeleteCargoCapacity(id);
        _crewUI.DeleteCrew(id);
        
        ShipmentDeleted?.Invoke($"Shipment with ID {id} has been deleted.");
    }
}