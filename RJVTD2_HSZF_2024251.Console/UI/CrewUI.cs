using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

// This class manages the user interface (UI) for crew-related operations (CRUD: Create, Read, Update, Delete)
public class CrewUI
{
    // Private field for the injected ICrewService dependency
    ICrewService _crewService;

    // Constructor that accepts an ICrewService instance for dependency injection
    public CrewUI(ICrewService crewService)
    {
        _crewService = crewService;
    }

    // Retrieves a Crew by its associated ShipmentId and displays its details
    public void GetCrewById(string id)
    {
        try
        {
            // Initialize crewId to store the matching crew's ID
            string? crewId = null;
            // Fetch all crews from the service
            IEnumerable<Crew> crews = _crewService.ReadAllCrews();

            // Search for the crew by ShipmentId
            foreach (Crew crew in crews)
            {
                if (crew.ShipmentId == id) crewId = crew.Id;
            }
            
            // Retrieve the crew by its ID from the service
            Crew crewById = _crewService.GetCrewById(crewId);
        
            // Display the crew information
            System.Console.WriteLine($"Information about the ship's crew:" +
                                     $"\n\t-Captain's name: {crewById.CaptainName}" +
                                     $"\n\t-Crew count: {crewById.CrewCount}");
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    // Retrieves and returns all crews from the service
    public IEnumerable<Crew> ReadAllCrews()
    {
        try
        {
            // Return all crews from the service
            return _crewService.ReadAllCrews();
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }

    // Creates a new Crew with user-provided data
    public void CreateCrew(string id)
    {
        try
        {
            // Collect the details to create a new Crew
            Crew crewToCreate = new Crew();
            crewToCreate.Id = id;
            crewToCreate.CaptainName = Commands.GetString("Enter the name of the crew's captain: ");
            crewToCreate.CrewCount = Commands.GetInt("Enter the size of the crew you want to create: ");
            crewToCreate.ShipmentId = id;
            // Call the service to create the Crew
            _crewService.CreateCrew(crewToCreate);
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Updates an existing Crew with new user-provided data
    public void UpdateCrew(string id)
    {
        try
        {
            // Initialize crewId to store the matching crew's ID
            string? crewId = null;
            // Fetch all crews from the service
            IEnumerable<Crew> crews = _crewService.ReadAllCrews();

            // Search for the crew by its ID
            foreach (Crew crew in crews)
            {
                if (crew.Id == id) crewId = crew.Id;
            }
        
            // Collect the details to update the Crew
            Crew crewToUpdate = new Crew();
            crewToUpdate.Id = crewId;
            crewToUpdate.CaptainName = Commands.GetString("Enter the name of the crew's captain: ");
            crewToUpdate.CrewCount = Commands.GetInt("Enter the size of the crew you want to update: ");
            crewToUpdate.ShipmentId = id;
            // Call the service to update the Crew
            _crewService.UpdateCrew(crewToUpdate);
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Deletes an existing Crew by its ID
    public void DeleteCrew(string id)
    {
        try
        {
            // Initialize crewId to store the matching crew's ID
            string? crewId = null;
            // Fetch all crews from the service
            IEnumerable<Crew> crews = _crewService.ReadAllCrews();

            // Search for the crew by its ID
            foreach (Crew crew in crews)
            {
                if (crew.Id == id) crewId = crew.Id;
            }
            // Call the service to delete the Crew
            _crewService.DeleteCrew(crewId);
        }
        catch (Exception ex)
        {
            // Handle any error that occurs during the process
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
}