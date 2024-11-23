using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

// Interface defining operations for managing Crew entities in the database
public interface ICrewDataProvider
{
    // Retrieves a Crew entity by its ID
    Crew GetCrewById(string crewId);
    
    // Creates a new Crew entity in the database
    void CreateCrew(Crew crew);
    
    // Returns all Crew entities from the database
    IEnumerable<Crew> ReadAllCrews();
    
    // Updates an existing Crew entity in the database
    void UpdateCrew(Crew crew);
    
    // Deletes a Crew entity by its ID
    void DeleteCrew(string crewId);
}

// Class implementation for the ICrewDataProvider interface
// Uses DeathStarDbContext for database operations on Crew entities
public class CrewDataProvider : ICrewDataProvider
{
    private readonly DeathStarDbContext _context;

    // Constructor that accepts a DbContext for dependency injection
    public CrewDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    // Retrieves a Crew entity by its ID
    public Crew GetCrewById(string crewId)
    {
        return _context.Crews.FirstOrDefault(t => t.Id.Equals(crewId));
    }

    // Creates and saves a new Crew entity in the database
    public void CreateCrew(Crew crew)
    {
        _context.Crews.Add(crew);
        _context.SaveChanges();
    }
    
    // Retrieves and returns all Crew entities
    public IEnumerable<Crew> ReadAllCrews()
    {
        return _context.Crews;
    }

    // Updates an existing Crew entity's properties in the database
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

    // Deletes a Crew entity by its ID, if it exists in the database
    public void DeleteCrew(string crewId)
    {
        Crew crewToDelete = _context.Crews.FirstOrDefault(t => t.Id.Equals(crewId));
        if (crewToDelete != null) _context.Crews.Remove(crewToDelete);

        _context.SaveChanges();
    }
}