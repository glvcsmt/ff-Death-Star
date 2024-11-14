using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

public interface ICargoDataProvider
{
    Cargo GetCargoById(int cargoId);
    
    void CreateCargo(Cargo cargo);
    
    IEnumerable<Cargo> ReadAllCargoes();
    
    void UpdateCargo(Cargo cargo);
    
    void DeleteCargo(int cargoId);
}

public class CargoDataProvider : ICargoDataProvider
{
    private readonly DeathStarDbContext _context;

    public CargoDataProvider(DeathStarDbContext context)
    {
        this._context = context;
    }
    
    public Cargo GetCargoById(int cargoId)
    {
        return _context.Cargoes.FirstOrDefault(t => t.Id.Equals(cargoId));
    }

    public void CreateCargo(Cargo cargo)
    {
        _context.Cargoes.Add(cargo);
        _context.SaveChanges();
    }

    public IEnumerable<Cargo> ReadAllCargoes()
    {
        return _context.Cargoes;
    }

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

    public void DeleteCargo(int cargoId)
    {
        Cargo cargoToDelete = _context.Cargoes.FirstOrDefault(t => t.Id.Equals(cargoId));
        if(cargoToDelete != null) _context.Cargoes.Remove(cargoToDelete);

        _context.SaveChanges();
    }
}