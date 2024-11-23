using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class CrewUI
{
    ICrewService _crewService;

    public CrewUI(ICrewService crewService)
    {
        _crewService = crewService;
    }

    public void GetCrewById()
    {
        string id = Commands.GetString("Enter the ID of the crew you want to get!\n");
        Crew crewById = _crewService.GetCrewById(id);
        
        System.Console.WriteLine($"Information about the crew that has the ID: {id}:" +
                                 $"\n\t-Captain's name: {crewById.CaptainName}" +
                                 $"\n\t-Crew count: {crewById.CrewCount}" +
                                 $"\n\t-Shipment's ID: {crewById.ShipmentId}");
    }

    public void CreateCrew()
    {
        Crew crewToCreate = new Crew();
        crewToCreate.Id = Commands.GetString("Enter the ID of the crew you want to create!\n");
        crewToCreate.CaptainName = Commands.GetString("Enter the name of the crew's captain!\n");
        crewToCreate.CrewCount = Commands.GetInt("Enter the size of the crew you want to create!\n");
        crewToCreate.ShipmentId = Commands.GetString("Enter the ID of the shipment the crew is managing!\n");
        _crewService.CreateCrew(crewToCreate);
        
        System.Console.WriteLine($"Crew with the ID of {crewToCreate.Id} was created successfully!");
    }

    public void UpdateCrew()
    {
        Crew crewToUpdate = new Crew();
        crewToUpdate.Id = Commands.GetString("Enter the ID of the crew you want to update!\n");
        crewToUpdate.CaptainName = Commands.GetString("Enter the name of the crew's captain!\n");
        crewToUpdate.CrewCount = Commands.GetInt("Enter the size of the crew you want to update!\n");
        crewToUpdate.ShipmentId = Commands.GetString("Enter the ID of the shipment the crew is managing!\n");
        _crewService.UpdateCrew(crewToUpdate);
        
        System.Console.WriteLine($"Crew with the ID of {crewToUpdate.Id} was updated successfully!");
    }

    public void DeleteCrew()
    {
        string id = Commands.GetString("Enter the ID of the crew you want to delete!\n");
        _crewService.DeleteCrew(id);
        
        System.Console.WriteLine($"Crew with the ID of {id} was deleted successfully!");
    }
}