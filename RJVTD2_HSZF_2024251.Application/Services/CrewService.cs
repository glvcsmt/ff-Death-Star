using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface ICrewService
{
    Crew GetCrewById(int crewId);
    
    void CreateCrew(Crew crew);
    
    IEnumerable<Crew> ReadAllCrews();
    
    void UpdateCrew(Crew crew);
    
    void DeleteCrew(int crewId);
}

public class CrewService : ICrewService
{
    private readonly ICrewDataProvider _crewDataProvider;

    public CrewService(ICrewDataProvider crewDataProvider)
    {
        this._crewDataProvider = crewDataProvider;
    }

    public Crew GetCrewById(int crewId)
    {
        return _crewDataProvider.GetCrewById(crewId);
    }

    public void CreateCrew(Crew crew)
    {
        _crewDataProvider.CreateCrew(crew);
    }

    public IEnumerable<Crew> ReadAllCrews()
    {
        return _crewDataProvider.ReadAllCrews();
    }

    public void UpdateCrew(Crew crew)
    {
        _crewDataProvider.UpdateCrew(crew);
    }

    public void DeleteCrew(int crewId)
    {
        _crewDataProvider.DeleteCrew(crewId);
    }
}