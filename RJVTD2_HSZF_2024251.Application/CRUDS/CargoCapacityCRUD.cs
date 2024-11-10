using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql;

namespace RJVTD2_HSZF_2024251.Application.CRUDS;

public class CargoCapacityCRUD
{
    DeathStarDbContext Context { get; set; }
    public CargoCapacityCRUD(DeathStarDbContext context)
    {
        Context = context;
    }

    public void CreateCargoCapacity(CargoCapacity cargoCapacity)
    {
        Context.CargoCapacities.Add(cargoCapacity);
        Context.SaveChanges();
    }

    public IEnumerable<CargoCapacity> ReadAllCargoCapacity()
    {
        return Context.CargoCapacities;
    }

    public void UpdateCargoCapacity(CargoCapacity cargoCapacity)
    {
        var cargoCapacityToUpdate = Context.CargoCapacities.FirstOrDefault(t => t.Id == cargoCapacity.Id);

        if (cargoCapacityToUpdate != null)
        {
            cargoCapacityToUpdate.Amount = cargoCapacity.Amount;
            cargoCapacityToUpdate.Unit = cargoCapacity.Unit;
            cargoCapacityToUpdate.ShipmentId = cargoCapacity.ShipmentId;
        }
        
        Context.SaveChanges();
    }

    public void DeleteCargoCapacity(CargoCapacity cargoCapacity)
    {
        var cargoCapacityToDelete = Context.CargoCapacities.FirstOrDefault(t => t.Id == cargoCapacity.Id);
        if (cargoCapacityToDelete != null) Context.CargoCapacities.Remove(cargoCapacityToDelete);
        
        Context.SaveChanges();
    }
}