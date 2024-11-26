using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class ShipmentUI
{
    private IShipmentService _shipmentService;
    private CrewUI _crewUI;
    private CargoCapacityUI _cargoCapacityUI;

    public ShipmentUI(IShipmentService shipmentService, ICrewService crewService, ICargoCapacityService cargoCapacityService)
    {
        _shipmentService = shipmentService;
        _crewUI = new CrewUI(crewService);
        _cargoCapacityUI = new CargoCapacityUI(cargoCapacityService);
    }

    public void GetShipmentById()
    {
        string id = Commands.GetString("Enter the ID of the shipment you want to get\n");
        Shipment shipmentById = _shipmentService.GetShipmentById(id);
        
        System.Console.WriteLine($"Information about the shipment that has the ID: {id}:" +
                                 $"\n\t-Type of the ship: {shipmentById.ShipType}" +
                                 $"\n\t-Date of the shipping: {shipmentById.ShipmentDate}" +
                                 $"\n\t-Current status of the ship: {shipmentById.Status}" +
                                 $"\n\t-Imperial Permit Number of the ship: {shipmentById.ImperialPermitNumber}");
        _cargoCapacityUI.GetCargoCapacityById(id);
        _crewUI.GetCrewById(id);
    }

    public void CreateShipment()
    {
        Shipment shipmentToCreate = new Shipment();
        shipmentToCreate.Id = Commands.GetString("Enter the ID of the shipment you want to create\n");
        shipmentToCreate.ShipType = Commands.GetString("Enter the ship type!\n");
        shipmentToCreate.Status = Commands.GetString("Enter the ship's status!\n");
        shipmentToCreate.ShipmentDate = DateTime.Parse(Commands.GetString("Enter the shipping date (yyyy-mm-dd)!\n"));
        shipmentToCreate.ImperialPermitNumber = Commands.GetString("Enter the Imperial Permit Number of the shipment!\n");
        _shipmentService.CreateShipment(shipmentToCreate);
        
        _cargoCapacityUI.CreateCargoCapacity(shipmentToCreate.Id);
        _crewUI.CreateCrew(shipmentToCreate.Id);
        
        System.Console.WriteLine($"Shipment with the ID of {shipmentToCreate.Id} was created successfully!");
        
        if (shipmentToCreate.ShipmentDate < DateTime.Now)
        {
            string updatedStatus = shipmentToCreate.Status += ", Shipment is late!";
            shipmentToCreate.Status = updatedStatus;
            _shipmentService.UpdateShipment(shipmentToCreate);
            //Event about the status
        }
    }

    public void UpdateShipment()
    {
        Shipment shipmentToUpdate = new Shipment();
        shipmentToUpdate.Id = Commands.GetString("Enter the ID of the shipment you want to update\n");
        shipmentToUpdate.ShipType = Commands.GetString("Enter the Ship Type!\n");
        shipmentToUpdate.ShipmentDate = DateTime.Parse(Commands.GetString("Enter the shipping date (yyyy-mm-dd)!\n"));
        shipmentToUpdate.ImperialPermitNumber = Commands.GetString("Enter the Imperial Permit Number of the shipment!\n");
        _shipmentService.UpdateShipment(shipmentToUpdate);
        
        _cargoCapacityUI.UpdateCargoCapacity(shipmentToUpdate.Id);
        _crewUI.UpdateCrew(shipmentToUpdate.Id);
        
        System.Console.WriteLine($"Shipment with the ID of {shipmentToUpdate.Id} was updated successfully!");
    }

    public void DeleteShipment()
    {
        string id = Commands.GetString("Enter the ID of the shipment you want to delete\n");
        _shipmentService.DeleteShipment(id);
        _cargoCapacityUI.DeleteCargoCapacity(id);
        _crewUI.DeleteCrew(id);
        
        System.Console.WriteLine($"Shipment with the ID of {id} was deleted successfully!");
    }
}