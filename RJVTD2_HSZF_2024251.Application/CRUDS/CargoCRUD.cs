using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql;

namespace RJVTD2_HSZF_2024251.Application.CRUDS;

public class CargoCRUD
{
    public DeathStarDbContext Context { get; set; }

    public CargoCRUD(DeathStarDbContext context)
    {
        Context = context;
    }

    public void CreateCargo(Cargo cargo)
    {
        Context.Cargoes.Add(cargo);
        Context.SaveChanges();
    }

    public IEnumerable<Cargo> ReadAllCargoes()
    {
        return Context.Cargoes;
    }

    public void UpdateCargo(Cargo cargo)
    {
        var cargoToUpdate = Context.Cargoes.FirstOrDefault(t => t.Id == cargo.Id);
        
        if (cargoToUpdate != null)
        {
            cargoToUpdate.CargoType = cargo.CargoType;
            cargoToUpdate.Quantity = cargo.Quantity;
            cargoToUpdate.ImperialCredits = cargo.ImperialCredits;
            cargoToUpdate.Insurance = cargo.Insurance;
            cargoToUpdate.RiskLevel = cargo.RiskLevel;
            cargoToUpdate.ShipmentId = cargo.ShipmentId;
        }
        
        Context.SaveChanges();
    }

    public void DeleteCargo(Cargo cargo)
    {
        var cargoToDelete = Context.Cargoes.FirstOrDefault(t => t.Id == cargo.Id);
        if(cargoToDelete != null) Context.Cargoes.Remove(cargoToDelete);
        
        Context.SaveChanges();
    }
}