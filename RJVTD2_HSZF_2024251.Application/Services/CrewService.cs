using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface defining operations for Crew service layer
public interface ICrewService
{
    // Retrieves a Crew entity by its ID
    Crew GetCrewById(string crewId);

    // Creates a new Crew entity
    void CreateCrew(Crew crew);

    // Returns all Crew entities
    IEnumerable<Crew> ReadAllCrews();

    // Updates an existing Crew entity
    void UpdateCrew(Crew crew);

    // Deletes a Crew entity by its ID
    void DeleteCrew(string crewId);
}

// Implementation of the ICrewService interface
// This service interacts with the ICrewDataProvider for data operations
public class CrewService : ICrewService
{
    private readonly ICrewDataProvider _crewDataProvider;

    // Constructor accepting an ICrewDataProvider instance for dependency injection
    public CrewService(ICrewDataProvider crewDataProvider)
    {
        _crewDataProvider = crewDataProvider;
    }

    // Retrieves a Crew entity by its ID
    public Crew GetCrewById(string crewId)
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
    public void DeleteCrew(string crewId)
    {
        _crewDataProvider.DeleteCrew(crewId);
    }
}