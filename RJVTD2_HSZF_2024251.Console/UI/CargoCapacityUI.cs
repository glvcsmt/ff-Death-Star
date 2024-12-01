using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

// This class provides methods for handling user interface (UI) interactions
// related to cargo capacity management (Create, Read, Update, Delete)
public class CargoCapacityUI
{
    // Private field for the injected ICargoCapacityService dependency
    ICargoCapacityService _cargoCapacityService;

    // Constructor that accepts an ICargoCapacityService instance
    // for dependency injection
    public CargoCapacityUI(ICargoCapacityService cargoCapacityService)
    {
        _cargoCapacityService = cargoCapacityService;
    }

    // Retrieves a CargoCapacity by its Shipment ID and displays it to the user
    public void GetCargoCapacityById(string id)
    {
        try
        {
            string? cargoCapacityId = null;
            // Retrieves all cargo capacities
            IEnumerable<CargoCapacity> cargoCapacities = _cargoCapacityService.ReadAllCargoCapacities();
        
            // Find the CargoCapacity with the matching ShipmentId
            foreach (CargoCapacity cargoCapacity in cargoCapacities)
            {
                if (cargoCapacity.ShipmentId == id) cargoCapacityId = cargoCapacity.Id;
            }
        
            // Get the full details of the CargoCapacity by ID
            CargoCapacity cargoCapacityById = _cargoCapacityService.GetCargoCapacityById(cargoCapacityId);
        
            // Display cargo capacity information
            System.Console.WriteLine($"Information about the ship's cargo capacity:" +
                                     $"\n\t-Measuring unit: {cargoCapacityById.Unit}" +
                                     $"\n\t-Maximum capacity: {cargoCapacityById.Amount}");
        }
        catch (Exception ex)
        {
            // Catch and display any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    // Retrieves and returns all CargoCapacities
    public IEnumerable<CargoCapacity> ReadAllCargoCapacities()
    {
        try
        {
            // Returns all CargoCapacities by calling the service method
            return _cargoCapacityService.ReadAllCargoCapacities();
        }
        catch (Exception ex)
        {
            // Catch and display any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }

    // Creates a new CargoCapacity with user input data
    public void CreateCargoCapacity(string id)
    {
        try
        {
            CargoCapacity cargoCapacityToCreate = new CargoCapacity();
            cargoCapacityToCreate.Id = id;
            // Get the unit in which the cargo is measured from the user
            cargoCapacityToCreate.Unit = Commands.GetString("Enter the Unit in which the ship's cargo is measured: ");
            // Get the maximum capacity from the user
            cargoCapacityToCreate.Amount = Commands.GetInt("Enter the maximum capacity: ");
            cargoCapacityToCreate.ShipmentId = id;
            // Call the service to create the CargoCapacity
            _cargoCapacityService.CreateCargoCapacity(cargoCapacityToCreate);
        }
        catch (Exception ex)
        {
            // Catch and display any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Updates an existing CargoCapacity with new user-provided data
    public void UpdateCargoCapacity(string id)
    {
        try
        {
            string? cargoCapacityId = null;
            // Retrieve all CargoCapacities
            IEnumerable<CargoCapacity> cargoCapacities = _cargoCapacityService.ReadAllCargoCapacities();
        
            // Find the CargoCapacity with the matching ID
            foreach (CargoCapacity cargoCapacity in cargoCapacities)
            {
                if (cargoCapacity.Id == id) cargoCapacityId = cargoCapacity.Id;
            }
        
            // Create a new CargoCapacity object to update with new data
            CargoCapacity cargoCapacityToUpdate = new CargoCapacity();
            cargoCapacityToUpdate.Id = cargoCapacityId;
            // Get the unit in which the cargo is measured from the user
            cargoCapacityToUpdate.Unit = Commands.GetString("Enter the Unit in which the cargo is measured: ");
            // Get the maximum capacity from the user
            cargoCapacityToUpdate.Amount = Commands.GetInt("Enter the maximum capacity: ");
            cargoCapacityToUpdate.ShipmentId = id;
            // Call the service to update the CargoCapacity
            _cargoCapacityService.UpdateCargoCapacity(cargoCapacityToUpdate);
        }
        catch (Exception ex)
        {
            // Catch and display any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Deletes an existing CargoCapacity by its ID
    public void DeleteCargoCapacity(string id)
    {
        try
        {
            string? cargoCapacityId = null;
            // Retrieve all CargoCapacities
            IEnumerable<CargoCapacity> cargoCapacities = _cargoCapacityService.ReadAllCargoCapacities();
        
            // Find the CargoCapacity with the matching ID
            foreach (CargoCapacity cargoCapacity in cargoCapacities)
            {
                if (cargoCapacity.Id == id) cargoCapacityId = cargoCapacity.Id;
            }
        
            // Call the service to delete the CargoCapacity
            _cargoCapacityService.DeleteCargoCapacity(cargoCapacityId);
        }
        catch (Exception ex)
        {
            // Catch and display any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
}