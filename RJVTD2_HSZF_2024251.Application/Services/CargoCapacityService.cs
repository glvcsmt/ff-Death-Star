using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.DataProviders;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface defining operations for CargoCapacity service layer
public interface ICargoCapacityService
{
    // Retrieves a CargoCapacity by its ID
    CargoCapacity GetCargoCapacityById(int cargoCapacityId);
    
    // Creates a new CargoCapacity
    void CreateCargoCapacity(CargoCapacity cargoCapacity);
    
    // Returns all CargoCapacities
    IEnumerable<CargoCapacity> ReadAllCargoCapacities();
    
    // Updates an existing CargoCapacity
    void UpdateCargoCapacity(CargoCapacity cargoCapacity);
    
    // Deletes a CargoCapacity by its ID
    void DeleteCargoCapacity(int cargoCapacityId);
}

// Implementation of the ICargoCapacityService interface
// This service interacts with the ICargoCapacityDataProvider for data operations
public class CargoCapacityService : ICargoCapacityDataProvider
{
    private readonly ICargoCapacityDataProvider _cargoCapacityDataProvider;

    // Constructor accepting an ICargoCapacityDataProvider instance for dependency injection
    public CargoCapacityService(ICargoCapacityDataProvider cargoCapacityDataProvider)
    {
        this._cargoCapacityDataProvider = cargoCapacityDataProvider;
    }
    
    // Retrieves a CargoCapacity by its ID
    public CargoCapacity GetCargoCapacityById(int cargoCapacityId)
    {
        return _cargoCapacityDataProvider.GetCargoCapacityById(cargoCapacityId);
    }

    // Creates a new CargoCapacity
    public void CreateCargoCapacity(CargoCapacity cargoCapacity)
    {
        _cargoCapacityDataProvider.CreateCargoCapacity(cargoCapacity);
    }

    // Retrieves and returns all CargoCapacities
    public IEnumerable<CargoCapacity> ReadAllCargoCapacities()
    {
        return _cargoCapacityDataProvider.ReadAllCargoCapacities();
    }

    // Updates an existing CargoCapacity
    public void UpdateCargoCapacity(CargoCapacity cargoCapacity)
    {
        _cargoCapacityDataProvider.UpdateCargoCapacity(cargoCapacity);
    }

    // Deletes a CargoCapacity by its ID
    public void DeleteCargoCapacity(int cargoCapacityId)
    {
        _cargoCapacityDataProvider.DeleteCargoCapacity(cargoCapacityId);
    }
}