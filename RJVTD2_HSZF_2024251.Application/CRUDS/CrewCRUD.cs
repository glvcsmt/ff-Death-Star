using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql;

namespace RJVTD2_HSZF_2024251.Application.CRUDS;

public class CrewCRUD
{
    DeathStarDbContext Context { get; set; }
    public CrewCRUD(DeathStarDbContext context)
    {
        Context = context;
    }

    public void CreateCrew(Crew crew)
    {
        Context.Crews.Add(crew);
        Context.SaveChanges();
    }

    public IEnumerable<Crew> ReadAllCrews()
    {
        return Context.Crews;
    }

    public void UpdateCrew(Crew crew)
    {
        var crewToUpdate = Context.Crews.FirstOrDefault(t => t.Id == crew.Id);

        if (crewToUpdate != null)
        {
            crewToUpdate.CrewCount = crew.CrewCount;
            crewToUpdate.CaptainName = crew.CaptainName;
            crewToUpdate.ShipmentId = crew.ShipmentId;
        }
        
        Context.SaveChanges();
    }

    public void DeleteCrew(Crew crew)
    {
        var crewToDelete = Context.Crews.FirstOrDefault(t => t.Id == crew.Id);
        if (crewToDelete != null) Context.Crews.Remove(crewToDelete);

        Context.SaveChanges();
    }
}