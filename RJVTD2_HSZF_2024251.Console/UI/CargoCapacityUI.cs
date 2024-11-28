using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class CargoCapacityUI
{
    ICargoCapacityService _cargoCapacityService;

    public CargoCapacityUI(ICargoCapacityService cargoCapacityService)
    {
        _cargoCapacityService = cargoCapacityService;
    }

    public void GetCargoCapacityById(string id)
    {
        string? cargoCapacityId = null;
        IEnumerable<CargoCapacity> cargoCapacities = _cargoCapacityService.ReadAllCargoCapacities();
        
        foreach (CargoCapacity cargoCapacity in cargoCapacities)
        {
            if (cargoCapacity.ShipmentId == id) cargoCapacityId = cargoCapacity.Id;
        }
        
        CargoCapacity cargoCapacityById = _cargoCapacityService.GetCargoCapacityById(cargoCapacityId);
        
        System.Console.WriteLine($"Information about the ship's cargo capacity:" +
                                 $"\n\t-Measuring unit: {cargoCapacityById.Unit}" +
                                 $"\n\t-Maximum capacity: {cargoCapacityById.Amount}");
    }

    public void CreateCargoCapacity(string id)
    {
        CargoCapacity cargoCapacityToCreate = new CargoCapacity();
        cargoCapacityToCreate.Id = id;
        cargoCapacityToCreate.Unit = Commands.GetString("Enter the Unit in which the ship's cargo is measured: ");
        cargoCapacityToCreate.Amount = Commands.GetInt("Enter the maximum capacity: ");
        cargoCapacityToCreate.ShipmentId = id;
        _cargoCapacityService.CreateCargoCapacity(cargoCapacityToCreate);
    }

    public void UpdateCargoCapacity(string id)
    {
        string? cargoCapacityId = null;
        IEnumerable<CargoCapacity> cargoCapacities = _cargoCapacityService.ReadAllCargoCapacities();
        
        foreach (CargoCapacity cargoCapacity in cargoCapacities)
        {
            if (cargoCapacity.Id == id) cargoCapacityId = cargoCapacity.Id;
        }
        
        CargoCapacity cargoCapacityToUpdate = new CargoCapacity();
        cargoCapacityToUpdate.Id = cargoCapacityId;
        cargoCapacityToUpdate.Unit = Commands.GetString("Enter the Unit in which the cargo is measured: ");
        cargoCapacityToUpdate.Amount = Commands.GetInt("Enter the maximum capacity: ");
        cargoCapacityToUpdate.ShipmentId = id;
        _cargoCapacityService.UpdateCargoCapacity(cargoCapacityToUpdate);
    }

    public void DeleteCargoCapacity(string id)
    {
        string? cargoCapacityId = null;
        IEnumerable<CargoCapacity> cargoCapacities = _cargoCapacityService.ReadAllCargoCapacities();
        
        foreach (CargoCapacity cargoCapacity in cargoCapacities)
        {
            if (cargoCapacity.Id == id) cargoCapacityId = cargoCapacity.Id;
        }
        
        _cargoCapacityService.DeleteCargoCapacity(cargoCapacityId);
    }
}