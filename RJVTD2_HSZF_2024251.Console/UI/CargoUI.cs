using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

// This class handles the user interface (UI) logic related to cargo management (CRUD operations)
public class CargoUI
{
    // Private field for the injected ICargoService dependency
    ICargoService _cargoService;
    
    // Events triggered after performing CRUD operations on Cargo
    public event Action<string> CargoCreated;
    public event Action<string> CargoUpdated;
    public event Action<string> CargoDeleted;

    // Constructor that accepts an ICargoService instance for dependency injection
    public CargoUI(ICargoService cargoService)
    {
        _cargoService = cargoService;
    }

    // Retrieves a Cargo by its ID and displays it to the user
    public void GetCargoById()
    {
        try
        {
            // Get the cargo ID from the user
            string id = Commands.GetString("Enter the ID of the cargo you want to get: ");
            // Fetch the Cargo by ID from the service
            Cargo cargoById = _cargoService.GetCargoById(id);
        
            // Display cargo information
            System.Console.WriteLine($"Information about the cargo that has the ID: {cargoById.Id}" +
                                     $"\n\t-Cargo type: {cargoById.CargoType}" +
                                     $"\n\t-Quantity: {cargoById.Quantity}" +
                                     $"\n\t-Value in Imperial Credits: {cargoById.ImperialCredits}" +
                                     $"\n\t-Insured? {cargoById.Insurance}" +
                                     $"\n\t-Risk level: {Enum.GetName(typeof(RiskLevel), cargoById.RiskLevel)}" +
                                     $"\n\t-Shipment's ID: {cargoById.ShipmentId}");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    // Retrieves and returns all Cargoes from the service
    public IEnumerable<Cargo> ReadAllCargoes()
    {
        try
        {
            // Return all cargoes by calling the service method
            return _cargoService.ReadAllCargoes();
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }

    // Creates a new Cargo with user input data
    public void CreateCargo()
    {
        try
        {
            // Collect the details to create a new Cargo
            Cargo cargoToCreate = new Cargo();
            cargoToCreate.Id = Commands.GetString("Enter the ID of the cargo you want to create: ");
            cargoToCreate.CargoType = Commands.GetString("Enter the cargo type you want to create: ");
            cargoToCreate.Quantity = Commands.GetInt("Enter the quantity of the cargo: ");
            cargoToCreate.ImperialCredits = Commands.GetInt("Enter the value of the cargo in imperial credits: ");
            cargoToCreate.Insurance = bool.Parse(Commands.GetString("Is the cargo insured? (true/false): "));
            cargoToCreate.RiskLevel = (RiskLevel)Enum.Parse(typeof(RiskLevel), 
                Commands.GetString("Enter the number of the risk level!" +
                                   "\n(0 = Low, 1 = Medium, 2 = High, 3 = Critical): "));
            cargoToCreate.ShipmentId = Commands.GetString("Enter the ID of the shipment the cargo belongs to: ");
            // Call the service to create the Cargo
            _cargoService.CreateCargo(cargoToCreate);
        
            // Trigger the CargoCreated event after creating the Cargo
            CargoCreated?.Invoke($"Cargo with ID {cargoToCreate.Id} has been created.");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Updates an existing Cargo with new user-provided data
    public void UpdateCargo()
    {
        try
        {
            // Collect the details to update an existing Cargo
            Cargo cargoToUpdate = new Cargo();
            cargoToUpdate.Id = Commands.GetString("Enter the ID of the crew you want to update: ");
            cargoToUpdate.CargoType = Commands.GetString("Enter the cargo type you want to update: ");
            cargoToUpdate.Quantity = Commands.GetInt("Enter the quantity of the cargo: ");
            cargoToUpdate.ImperialCredits = Commands.GetInt("Enter the value of the cargo in imperial credits: ");
            cargoToUpdate.Insurance = bool.Parse(Commands.GetString("Is the cargo insured? (true/false): "));
            cargoToUpdate.RiskLevel = (RiskLevel)Enum.Parse(typeof(RiskLevel), 
                Commands.GetString("Enter the number of the risk level!" +
                                   "\n(0 = Low, 1 = Medium, 2 = High, 3 = Critical): "));
            cargoToUpdate.ShipmentId = Commands.GetString("Enter the ID of the shipment the cargo belongs to: ");
            // Call the service to update the Cargo
            _cargoService.CreateCargo(cargoToUpdate);
        
            // Trigger the CargoUpdated event after updating the Cargo
            CargoUpdated?.Invoke($"Cargo with ID {cargoToUpdate.Id} has been updated.");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Deletes an existing Cargo by its ID
    public void DeleteCargo()
    {
        try
        {
            // Get the cargo ID from the user
            string id = Commands.GetString("Enter the ID of the cargo you want to delete: ");
            // Call the service to delete the Cargo
            _cargoService.DeleteCargo(id);
        
            //Deleted event
            // Trigger the CargoDeleted event after deleting the Cargo
            CargoDeleted?.Invoke($"Cargo with ID {id} has been deleted.");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
}