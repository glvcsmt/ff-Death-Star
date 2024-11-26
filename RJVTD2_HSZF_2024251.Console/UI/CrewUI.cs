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

    public void GetCrewById(string id)
    {
        string? crewId = null;
        IEnumerable<Crew> crews = _crewService.ReadAllCrews();

        foreach (Crew crew in crews)
        {
            if (crew.Id == id) crewId = crew.Id;
        }
        
        Crew crewById = _crewService.GetCrewById(crewId);
        
        System.Console.WriteLine($"Information about the ship's crew:" +
                                 $"\n\t-Captain's name: {crewById.CaptainName}" +
                                 $"\n\t-Crew count: {crewById.CrewCount}");
    }

    public void CreateCrew(string id)
    {
        Crew crewToCreate = new Crew();
        crewToCreate.Id = id;
        crewToCreate.CaptainName = Commands.GetString("Enter the name of the crew's captain!\n");
        crewToCreate.CrewCount = Commands.GetInt("Enter the size of the crew you want to create!\n");
        crewToCreate.ShipmentId = id;
        _crewService.CreateCrew(crewToCreate);
    }

    public void UpdateCrew(string id)
    {
        string? crewId = null;
        IEnumerable<Crew> crews = _crewService.ReadAllCrews();

        foreach (Crew crew in crews)
        {
            if (crew.Id == id) crewId = crew.Id;
        }
        
        Crew crewToUpdate = new Crew();
        crewToUpdate.Id = crewId;
        crewToUpdate.CaptainName = Commands.GetString("Enter the name of the crew's captain!\n");
        crewToUpdate.CrewCount = Commands.GetInt("Enter the size of the crew you want to update!\n");
        crewToUpdate.ShipmentId = id;
        _crewService.UpdateCrew(crewToUpdate);
    }

    public void DeleteCrew(string id)
    {
        string? crewId = null;
        IEnumerable<Crew> crews = _crewService.ReadAllCrews();

        foreach (Crew crew in crews)
        {
            if (crew.Id == id) crewId = crew.Id;
        }
        _crewService.DeleteCrew(crewId);
    }
}