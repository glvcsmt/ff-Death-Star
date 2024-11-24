using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class CargoCapacityUI
{
    ICargoCapacityService _cargoCapacityService;

    public CargoCapacityUI(ICargoCapacityService cargoCapacityService)
    {
        _cargoCapacityService = cargoCapacityService;
    }

    public void GetCargoCapacityById()
    {
        string id = Commands.GetString("Enter the ID of the cargo capacity you want to get!\n");
        CargoCapacity cargoCapacityById = _cargoCapacityService.GetCargoCapacityById(id);
        
        System.Console.WriteLine($"Information about the cargo capacity that has the ID: {cargoCapacityById}" +
                                 $"\n\t-Measuring unit: {cargoCapacityById.Unit}" +
                                 $"\n\t-Maximum capacity: {cargoCapacityById.Amount}" +
                                 $"\n\t-Shipment's ID: {cargoCapacityById.ShipmentId}");
    }

    public void CreateCargoCapacity()
    {
        CargoCapacity cargoCapacityToCreate = new CargoCapacity();
        cargoCapacityToCreate.Id = Commands.GetString("Enter the ID of the cargo capacity you want to create!\n");
        cargoCapacityToCreate.Unit = Commands.GetString("Enter the Unit in which the cargo is measured!\n");
        cargoCapacityToCreate.Amount = Commands.GetInt("Enter the maximum capacity!\n");
        cargoCapacityToCreate.ShipmentId = Commands.GetString("Enter the ID of the shipment it belongs to!\n");
        _cargoCapacityService.CreateCargoCapacity(cargoCapacityToCreate);
        
        System.Console.WriteLine($"Cargo capacity with the ID of {cargoCapacityToCreate.Id} was created successfully!");
    }

    public void UpdateCargoCapacity()
    {
        CargoCapacity cargoCapacityToUpdate = new CargoCapacity();
        cargoCapacityToUpdate.Id = Commands.GetString("Enter the ID of the cargo capacity you want to update!\n");
        cargoCapacityToUpdate.Unit = Commands.GetString("Enter the Unit in which the cargo is measured!\n");
        cargoCapacityToUpdate.Amount = Commands.GetInt("Enter the maximum capacity!\n");
        cargoCapacityToUpdate.ShipmentId = Commands.GetString("Enter the ID of the shipment it belongs to!\n");
        _cargoCapacityService.UpdateCargoCapacity(cargoCapacityToUpdate);
        
        System.Console.WriteLine($"Crew with the ID of {cargoCapacityToUpdate.Id} was updated successfully!");
    }

    public void DeleteCargoCapacity()
    {
        string id = Commands.GetString("Enter the ID of the cargo capacity you want to delete!\n");
        _cargoCapacityService.DeleteCargoCapacity(id);
        
        System.Console.WriteLine($"Crew with the ID of {id} was deleted successfully!");
    }
}