using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface ICargoCapacityService
{
    CargoCapacity GetCargoCapacityById(int cargoCapacityId);
    
    void CreateCargoCapacity(CargoCapacity cargoCapacity);
    
    IEnumerable<CargoCapacity> ReadAllCargoCapacities();
    
    void UpdateCargoCapacity(CargoCapacity cargoCapacity);
    
    void DeleteCargoCapacity(int cargoCapacityId);
}

public class CargoCapacityService : ICargoCapacityDataProvider
{
    private readonly ICargoCapacityDataProvider _cargoCapacityDataProvider;

    public CargoCapacityService(ICargoCapacityDataProvider cargoCapacityDataProvider)
    {
        this._cargoCapacityDataProvider = cargoCapacityDataProvider;
    }
    
    public CargoCapacity GetCargoCapacityById(int cargoCapacityId)
    {
        return _cargoCapacityDataProvider.GetCargoCapacityById(cargoCapacityId);
    }

    public void CreateCargoCapacity(CargoCapacity cargoCapacity)
    {
        _cargoCapacityDataProvider.CreateCargoCapacity(cargoCapacity);
    }

    public IEnumerable<CargoCapacity> ReadAllCargoCapacities()
    {
        return _cargoCapacityDataProvider.ReadAllCargoCapacities();
    }

    public void UpdateCargoCapacity(CargoCapacity cargoCapacity)
    {
        _cargoCapacityDataProvider.UpdateCargoCapacity(cargoCapacity);
    }

    public void DeleteCargoCapacity(int cargoCapacityId)
    {
        _cargoCapacityDataProvider.DeleteCargoCapacity(cargoCapacityId);
    }
}