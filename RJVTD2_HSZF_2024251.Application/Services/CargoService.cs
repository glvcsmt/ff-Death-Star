﻿using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface defining operations for Cargo service layer
public interface ICargoService
{
    // Retrieves a Cargo by its ID
    Cargo GetCargoById(int cargoId);
    
    // Creates a new Cargo
    void CreateCargo(Cargo cargo);
    
    // Returns all Cargo entities
    IEnumerable<Cargo> ReadAllCargoes();
    
    // Updates an existing Cargo
    void UpdateCargo(Cargo cargo);
    
    // Deletes a Cargo by its ID
    void DeleteCargo(int cargoId);
}

// Implementation of the ICargoService interface
// This service interacts with the ICargoDataProvider for data operations
public class CargoService : ICargoService
{
    private readonly ICargoDataProvider _cargoDataProvider;

    // Constructor accepting an ICargoDataProvider instance for dependency injection
    public CargoService(ICargoDataProvider cargoDataProvider)
    {
        this._cargoDataProvider = cargoDataProvider;
    }
    
    // Retrieves a Cargo by its ID
    public Cargo GetCargoById(int cargoId)
    {
        return _cargoDataProvider.GetCargoById(cargoId);
    }
    
    // Creates a new Cargo
    public void CreateCargo(Cargo cargo)
    {
        _cargoDataProvider.CreateCargo(cargo);
    }

    // Retrieves and returns all Cargo entities
    public IEnumerable<Cargo> ReadAllCargoes()
    {
        return _cargoDataProvider.ReadAllCargoes();
    }

    // Updates an existing Cargo
    public void UpdateCargo(Cargo cargo)
    {
        _cargoDataProvider.UpdateCargo(cargo);
    }

    // Deletes a Cargo by its ID
    public void DeleteCargo(int cargoId)
    {
        _cargoDataProvider.DeleteCargo(cargoId);
    }
}