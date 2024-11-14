using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

public interface ICrewDataProvider
{
    Crew GetCrewById(int crewId);
    
    void CreateCrew(Crew crew);
    
    IEnumerable<Crew> ReadAllCrews();
    
    void UpdateCrew(Crew crew);
    
    void DeleteCrew(int crewId);
}

public class CrewDataProvider : ICrewDataProvider
{
    private readonly DeathStarDbContext _context;

    public CrewDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    public Crew GetCrewById(int crewId)
    {
        return _context.Crews.FirstOrDefault(t => t.Id.Equals(crewId));
    }

    public void CreateCrew(Crew crew)
    {
        _context.Crews.Add(crew);
        _context.SaveChanges();
    }

    public IEnumerable<Crew> ReadAllCrews()
    {
        return _context.Crews;
    }

    public void UpdateCrew(Crew crew)
    {
        Crew crewToUpdate = _context.Crews.FirstOrDefault(t => t.Id == crew.Id);

        if (crewToUpdate != null)
        {
            crewToUpdate.CrewCount = crew.CrewCount;
            crewToUpdate.CaptainName = crew.CaptainName;
            crewToUpdate.ShipmentId = crew.ShipmentId;
        }
        
        _context.SaveChanges();
    }

    public void DeleteCrew(int crewId)
    {
        Crew crewToDelete = _context.Crews.FirstOrDefault(t => t.Id.Equals(crewId));
        if (crewToDelete != null) _context.Crews.Remove(crewToDelete);

        _context.SaveChanges();
    }
}