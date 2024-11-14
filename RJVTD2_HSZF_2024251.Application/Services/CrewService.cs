using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface defining operations for Crew service layer
public interface ICrewService
{
    // Retrieves a Crew entity by its ID
    Crew GetCrewById(int crewId);
    
    // Creates a new Crew entity
    void CreateCrew(Crew crew);
    
    // Returns all Crew entities
    IEnumerable<Crew> ReadAllCrews();
    
    // Updates an existing Crew entity
    void UpdateCrew(Crew crew);
    
    // Deletes a Crew entity by its ID
    void DeleteCrew(int crewId);
}

// Implementation of the ICrewService interface
// This service interacts with the ICrewDataProvider for data operations
public class CrewService : ICrewService
{
    private readonly ICrewDataProvider _crewDataProvider;

    // Constructor accepting an ICrewDataProvider instance for dependency injection
    public CrewService(ICrewDataProvider crewDataProvider)
    {
        this._crewDataProvider = crewDataProvider;
    }

    // Retrieves a Crew entity by its ID
    public Crew GetCrewById(int crewId)
    {
        return _crewDataProvider.GetCrewById(crewId);
    }

    // Creates a new Crew entity
    public void CreateCrew(Crew crew)
    {
        _crewDataProvider.CreateCrew(crew);
    }

    // Retrieves and returns all Crew entities
    public IEnumerable<Crew> ReadAllCrews()
    {
        return _crewDataProvider.ReadAllCrews();
    }
    
    // Updates an existing Crew entity
    public void UpdateCrew(Crew crew)
    {
        _crewDataProvider.UpdateCrew(crew);
    }

    // Deletes a Crew entity by its ID
    public void DeleteCrew(int crewId)
    {
        _crewDataProvider.DeleteCrew(crewId);
    }
}