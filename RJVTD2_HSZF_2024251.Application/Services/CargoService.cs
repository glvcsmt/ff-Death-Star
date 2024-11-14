using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface ICargoService
{
    Cargo GetCargoById(int cargoId);
    
    void CreateCargo(Cargo cargo);
    
    IEnumerable<Cargo> ReadAllCargoes();
    
    void UpdateCargo(Cargo cargo);
    
    void DeleteCargo(int cargoId);
}

public class CargoService : ICargoService
{
    private readonly ICargoDataProvider _cargoDataProvider;

    public CargoService(ICargoDataProvider cargoDataProvider)
    {
        this._cargoDataProvider = cargoDataProvider;
    }
    
    public Cargo GetCargoById(int cargoId)
    {
        return _cargoDataProvider.GetCargoById(cargoId);
    }

    public void CreateCargo(Cargo cargo)
    {
        _cargoDataProvider.CreateCargo(cargo);
    }

    public IEnumerable<Cargo> ReadAllCargoes()
    {
        return _cargoDataProvider.ReadAllCargoes();
    }

    public void UpdateCargo(Cargo cargo)
    {
        _cargoDataProvider.UpdateCargo(cargo);
    }

    public void DeleteCargo(int cargoId)
    {
        _cargoDataProvider.DeleteCargo(cargoId);
    }
}