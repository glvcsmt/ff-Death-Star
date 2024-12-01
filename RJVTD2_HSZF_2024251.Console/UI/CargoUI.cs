﻿using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Console.UI;

public class CargoUI
{
    ICargoService _cargoService;
    
    public event Action<string> CargoCreated;
    public event Action<string> CargoUpdated;
    public event Action<string> CargoDeleted;

    public CargoUI(ICargoService cargoService)
    {
        _cargoService = cargoService;
    }

    public void GetCargoById()
    {
        try
        {
            string id = Commands.GetString("Enter the ID of the cargo you want to get: ");
            Cargo cargoById = _cargoService.GetCargoById(id);
        
            System.Console.WriteLine($"Information about the cargo that has the ID: {cargoById.Id}" +
                                     $"\n\t-Cargo type: {cargoById.CargoType}" +
                                     $"\n\t-Quantity: {cargoById.Quantity}" +
                                     $"\n\t-Value in Imperial Credits: {cargoById.ImperialCredits}" +
                                     $"\n\t-Insured? {cargoById.Insurance}" +
                                     $"\n\t-Risk level: {Enum.GetName(typeof(RiskLevel), cargoById.RiskLevel)}" +
                                     $"\n\t-Shipment's ID: {cargoById.ShipmentId}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    public IEnumerable<Cargo> ReadAllCargoes()
    {
        try
        {
            return _cargoService.ReadAllCargoes();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }

    public void CreateCargo()
    {
        try
        {
            Cargo cargoToCreate = new Cargo();
            cargoToCreate.Id = Commands.GetString("Enter the ID of the cargo you want to create: ");
            cargoToCreate.CargoType = Commands.GetString("Enter the cargo type you want to create: ");
            cargoToCreate.Quantity = Commands.GetInt("Enter the quantity of the cargo: ");
            cargoToCreate.ImperialCredits = Commands.GetInt("Enter the value of the cargo in imperial credits: ");
            cargoToCreate.Insurance = bool.Parse(Commands.GetString("Is the cargo insured? (true/false): "));
            cargoToCreate.RiskLevel = (RiskLevel)Enum.Parse(typeof(RiskLevel), 
                Commands.GetString("Enter the number of the risk level!" +
                                   "\n(0 = Low, 1 = Medium, 2 = High, 3 = Critical): "));
            cargoToCreate.ShipmentId = Commands.GetString("Enter the ID of the shipment the cargo belongs to: ");
            _cargoService.CreateCargo(cargoToCreate);
        
            CargoCreated?.Invoke($"Cargo with ID {cargoToCreate.Id} has been created.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void UpdateCargo()
    {
        try
        {
            Cargo cargoToUpdate = new Cargo();
            cargoToUpdate.Id = Commands.GetString("Enter the ID of the crew you want to update: ");
            cargoToUpdate.CargoType = Commands.GetString("Enter the cargo type you want to update: ");
            cargoToUpdate.Quantity = Commands.GetInt("Enter the quantity of the cargo: ");
            cargoToUpdate.ImperialCredits = Commands.GetInt("Enter the value of the cargo in imperial credits: ");
            cargoToUpdate.Insurance = bool.Parse(Commands.GetString("Is the cargo insured? (true/false): "));
            cargoToUpdate.RiskLevel = (RiskLevel)Enum.Parse(typeof(RiskLevel), 
                Commands.GetString("Enter the number of the risk level!" +
                                   "\n(0 = Low, 1 = Medium, 2 = High, 3 = Critical): "));
            cargoToUpdate.ShipmentId = Commands.GetString("Enter the ID of the shipment the cargo belongs to: ");
            _cargoService.CreateCargo(cargoToUpdate);
        
            CargoUpdated?.Invoke($"Cargo with ID {cargoToUpdate.Id} has been updated.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void DeleteCargo()
    {
        try
        {
            string id = Commands.GetString("Enter the ID of the cargo you want to delete: ");
            _cargoService.DeleteCargo(id);
        
            //Deleted event
            CargoDeleted?.Invoke($"Cargo with ID {id} has been deleted.");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error: {ex.Message}");
        }
    }
}