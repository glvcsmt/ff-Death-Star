using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

public interface ICargoCapacityDataProvider
{
    CargoCapacity GetCargoCapacityById(int cargoCapacityId);
    
    void CreateCargoCapacity(CargoCapacity cargoCapacity);
    
    IEnumerable<CargoCapacity> ReadAllCargoCapacities();
    
    void UpdateCargoCapacity(CargoCapacity cargoCapacity);
    
    void DeleteCargoCapacity(int cargoCapacityId);
}

public class CargoCapacityDataProvider : ICargoCapacityDataProvider
{
    private readonly DeathStarDbContext _context;

    public CargoCapacityDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    public CargoCapacity GetCargoCapacityById(int cargoCapacityId)
    {
        return _context.CargoCapacities.FirstOrDefault(t => t.Id.Equals(cargoCapacityId));
    }

    public void CreateCargoCapacity(CargoCapacity cargoCapacity)
    {
        _context.CargoCapacities.Add(cargoCapacity);
        _context.SaveChanges();
    }

    public IEnumerable<CargoCapacity> ReadAllCargoCapacities()
    {
        return _context.CargoCapacities;
    }

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

    public void DeleteCargoCapacity(int cargoCapacityId)
    {
        CargoCapacity cargoCapacityToDelete = _context.CargoCapacities.FirstOrDefault(t => t.Id.Equals(cargoCapacityId));
        if (cargoCapacityToDelete != null) _context.CargoCapacities.Remove(cargoCapacityToDelete);
    }
}