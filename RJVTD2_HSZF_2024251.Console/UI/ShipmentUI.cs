using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

// The ShipmentUI class is responsible for managing user interface interactions related to shipments.
// It allows the user to create, read, update, and delete shipments, as well as handle related entities like Cargo Capacity, Crew, and Directory.
public class ShipmentUI
{
    // Private fields to hold service dependencies for shipment, crew, cargo capacity, and directory operations.
    private IShipmentService _shipmentService;
    private CrewUI _crewUI;
    private CargoCapacityUI _cargoCapacityUI;
    private IDirectoryService _directoryService;
    
    // Events to notify when a shipment is created, updated, deleted, or is late.
    public event Action<string> ShipmentCreated;
    public event Action<string> ShipmentUpdated;
    public event Action<string> ShipmentDeleted;
    public event Action<string> ShipmentLate;

    // Constructor to initialize the ShipmentUI with its dependencies via dependency injection.
    // This ensures that the necessary services for shipment, crew, cargo capacity, and directory are available.
   public ShipmentUI(IShipmentService shipmentService, ICrewService crewService, ICargoCapacityService cargoCapacityService, IDirectoryService directoryService)
    {
        _shipmentService = shipmentService;
        _crewUI = new CrewUI(crewService);
        _cargoCapacityUI = new CargoCapacityUI(cargoCapacityService);
        _directoryService = directoryService;
    }

    // Retrieves a shipment by its ID and displays detailed information about the shipment.
    // Also, shows related cargo capacity and crew details for the specified shipment ID.
    public void GetShipmentById()
    {
        try
        {
            string id = Commands.GetString("Enter the ID of the shipment you want to get: ");
            Shipment shipmentById = _shipmentService.GetShipmentById(id);
        
            // Display shipment details in the console
            System.Console.WriteLine($"Information about the shipment that has the ID: {id}:" +
                                     $"\n\t-Type of the ship: {shipmentById.ShipType}" +
                                     $"\n\t-Date of the shipping: {shipmentById.ShipmentDate}" +
                                     $"\n\t-Current status of the ship: {shipmentById.Status}" +
                                     $"\n\t-Imperial Permit Number of the ship: {shipmentById.ImperialPermitNumber}");
            _cargoCapacityUI.GetCargoCapacityById(id);
            _crewUI.GetCrewById(id);
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    // Reads and returns all shipments from the shipment service.
    public IEnumerable<Shipment> ReadAllShipments()
    {
        try
        {
            return _shipmentService.ReadAllShipments();
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }

    // Creates a new shipment by gathering user input for necessary details.
    // Invokes creation of related cargo capacity, crew, and directory as part of the shipment creation process.
    public void CreateShipment()
    {
        try
        {
            Shipment shipmentToCreate = new Shipment();
            shipmentToCreate.Id = Commands.GetString("Enter the ID of the shipment you want to create: ");
            shipmentToCreate.ShipType = Commands.GetString("Enter the ship type: ");
            shipmentToCreate.Status = Commands.GetString("Enter the ship's status: ");
            shipmentToCreate.ShipmentDate = DateTime.Parse(Commands.GetString("Enter the shipping date (yyyy-mm-dd): "));
            shipmentToCreate.ImperialPermitNumber = Commands.GetString("Enter the Imperial Permit Number of the shipment: ");
            _shipmentService.CreateShipment(shipmentToCreate);
        
            // Creating related entities for cargo capacity and crew associated with the new shipment
            _cargoCapacityUI.CreateCargoCapacity(shipmentToCreate.Id);
            _crewUI.CreateCrew(shipmentToCreate.Id);
        
            // Notify that the shipment has been created.
            ShipmentCreated?.Invoke($"Shipment with ID {shipmentToCreate.Id} has been created.");
        
            // If the shipment date is in the past, mark the shipment as late by appending to its status.
            if (shipmentToCreate.ShipmentDate.Value.Date < DateTime.Now.Date)
            {
                string updatedStatus = shipmentToCreate.Status += ", Shipment is late!";
                shipmentToCreate.Status = updatedStatus;
                _shipmentService.UpdateShipment(shipmentToCreate);
                ShipmentLate?.Invoke($"Shipment with ID {shipmentToCreate.Id} is late, status modified!");
            }

            // Create a directory using the shipment's Imperial Permit Number.
            _directoryService.CreateDirectory(shipmentToCreate.ImperialPermitNumber);
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Updates an existing shipment by collecting user input for shipment details.
    // Also triggers updates for related entities like cargo capacity and crew.
    public void UpdateShipment()
    {
        try
        {
            Shipment shipmentToUpdate = new Shipment();
            shipmentToUpdate.Id = Commands.GetString("Enter the ID of the shipment you want to update: ");
            shipmentToUpdate.ShipType = Commands.GetString("Enter the Ship Type: ");
            shipmentToUpdate.ShipmentDate = DateTime.Parse(Commands.GetString("Enter the shipping date (yyyy-mm-dd): "));
            _shipmentService.UpdateShipment(shipmentToUpdate);
        
            // Trigger updates for related cargo capacity and crew entities.
            _cargoCapacityUI.UpdateCargoCapacity(shipmentToUpdate.Id);
            _crewUI.UpdateCrew(shipmentToUpdate.Id);
        
            // Notify that the shipment has been updated.
            ShipmentUpdated?.Invoke($"Shipment with ID {shipmentToUpdate.Id} has been updated.");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Deletes an existing shipment and performs cleanup operations by deleting related cargo capacity, crew, and directory.
    public void DeleteShipment()
    {
        try
        {
            string id = Commands.GetString("Enter the ID of the shipment you want to delete: ");
        
            // Get the shipment details to delete its associated directory.
            Shipment directoryToDelete = _shipmentService.GetShipmentById(id);
            _directoryService.DeleteDirectory(directoryToDelete.ImperialPermitNumber);
        
            // Proceed with the deletion of the shipment and its related entities.
            _shipmentService.DeleteShipment(id);
            _cargoCapacityUI.DeleteCargoCapacity(id);
            _crewUI.DeleteCrew(id);
        
            // Notify that the shipment has been deleted.
            ShipmentDeleted?.Invoke($"Shipment with ID {id} has been deleted.");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
}