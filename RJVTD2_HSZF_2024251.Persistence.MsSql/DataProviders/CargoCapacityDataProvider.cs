using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

// Interface defining operations for managing CargoCapacity entities in the database
public interface ICargoCapacityDataProvider
{
    // Retrieves a CargoCapacity entity by its ID
    CargoCapacity GetCargoCapacityById(int cargoCapacityId);
    
    // Creates a new CargoCapacity entity in the database
    void CreateCargoCapacity(CargoCapacity cargoCapacity);
    
    // Returns all CargoCapacity entities from the database
    IEnumerable<CargoCapacity> ReadAllCargoCapacities();
    
    // Updates an existing CargoCapacity entity in the database
    void UpdateCargoCapacity(CargoCapacity cargoCapacity);
    
    // Deletes a CargoCapacity entity by its ID
    void DeleteCargoCapacity(int cargoCapacityId);
}

// Class implementation for the ICargoCapacityDataProvider interface
// Uses DeathStarDbContext for database operations on CargoCapacity entities
public class CargoCapacityDataProvider : ICargoCapacityDataProvider
{
    private readonly DeathStarDbContext _context;

    // Constructor that accepts a DbContext for dependency injection
    public CargoCapacityDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    // Retrieves a CargoCapacity entity by its ID
    public CargoCapacity GetCargoCapacityById(int cargoCapacityId)
    {
        return _context.CargoCapacities.FirstOrDefault(t => t.Id.Equals(cargoCapacityId));
    }

    // Creates and saves a new CargoCapacity entity in the database
    public void CreateCargoCapacity(CargoCapacity cargoCapacity)
    {
        _context.CargoCapacities.Add(cargoCapacity);
        _context.SaveChanges();
    }

    // Retrieves and returns all CargoCapacity entities
    public IEnumerable<CargoCapacity> ReadAllCargoCapacities()
    {
        return _context.CargoCapacities;
    }

    // Updates an existing CargoCapacity entity's properties in the database
    public void UpdateCargoCapacity(CargoCapacity cargoCapacity)
    {
        CargoCapacity cargoCapacityToUpdate = _context.CargoCapacities.FirstOrDefault(t => t.Id == cargoCapacity.Id);

        if (cargoCapacityToUpdate != null)
        {
            cargoCapacityToUpdate.Amount = cargoCapacity.Amount;
            cargoCapacityToUpdate.Unit = cargoCapacity.Unit;
            cargoCapacityToUpdate.ShipmentId = cargoCapacity.ShipmentId;
        }
        
        _context.SaveChanges();
    }
    
    // Updates an existing CargoCapacity entity's properties in the database
    public void DeleteCargoCapacity(int cargoCapacityId)
    {
        CargoCapacity cargoCapacityToDelete = _context.CargoCapacities.FirstOrDefault(t => t.Id.Equals(cargoCapacityId));
        if (cargoCapacityToDelete != null) _context.CargoCapacities.Remove(cargoCapacityToDelete);
    }
}