using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

// Interface defining operations for managing Cargo entities in the database
public interface ICargoDataProvider
{
    // Retrieves a Cargo entity by its ID
    Cargo GetCargoById(int cargoId);
    
    // Creates a new Cargo entity in the database
    void CreateCargo(Cargo cargo);
    
    // Returns all Cargo entities from the database
    IEnumerable<Cargo> ReadAllCargoes();
    
    // Updates an existing Cargo entity in the database
    void UpdateCargo(Cargo cargo);
    
    // Deletes a Cargo entity by its ID
    void DeleteCargo(int cargoId);
}

// Class implementation for the ICargoDataProvider interface
// Uses DeathStarDbContext for database operations on Cargo entities
public class CargoDataProvider : ICargoDataProvider
{
    private readonly DeathStarDbContext _context;

    // Constructor that accepts a DbContext for dependency injection
    public CargoDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    // Retrieves a Cargo entity by its ID
    public Cargo GetCargoById(int cargoId)
    {
        return _context.Cargoes.FirstOrDefault(t => t.Id.Equals(cargoId));
    }

    // Creates and saves a new Cargo entity in the database
    public void CreateCargo(Cargo cargo)
    {
        _context.Cargoes.Add(cargo);
        _context.SaveChanges();
    }

    // Retrieves and returns all Cargo entities
    public IEnumerable<Cargo> ReadAllCargoes()
    {
        return _context.Cargoes;
    }

    // Updates an existing Cargo entity's properties in the database
    public void UpdateCargo(Cargo cargo)
    {
        Cargo cargoToUpdate = _context.Cargoes.FirstOrDefault(t => t.Id == cargo.Id);

        if (cargoToUpdate != null)
        {
            cargoToUpdate.CargoType = cargo.CargoType;
            cargoToUpdate.Quantity = cargo.Quantity;
            cargoToUpdate.ImperialCredits = cargo.ImperialCredits;
            cargoToUpdate.Insurance = cargo.Insurance;
            cargoToUpdate.RiskLevel = cargo.RiskLevel;
            cargoToUpdate.ShipmentId = cargo.ShipmentId;
        }
        
        _context.SaveChanges();
    }

    // Deletes a Cargo entity by its ID, if it exists in the database
    public void DeleteCargo(int cargoId)
    {
        Cargo cargoToDelete = _context.Cargoes.FirstOrDefault(t => t.Id.Equals(cargoId));
        if(cargoToDelete != null) _context.Cargoes.Remove(cargoToDelete);

        _context.SaveChanges();
    }
}