using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData.Class;

//Class that converts the JSON oject to a Shipment object
public class ShipmentMapper
{
    public Shipment Map(Shipment seed)
    {
        //Maps the cargo capacity data to a CargoCapacity object
        var cargoCapacity = new CargoCapacity
        {
            Unit = seed.CargoCapacity.Unit,
            Amount = seed.CargoCapacity.Amount
        };
        
        //Maps the crew data to a Crew object
        var crew = new Crew
        {
            CaptainName = seed.Crew.CaptainName,
            CrewCount = seed.Crew.CrewCount
        };
        
        //Maps and assambles the Shipment object with the given data
        var shipment = new Shipment
        {
            ShipType = seed.ShipType,
            ShipmentDate = seed.ShipmentDate,
            CargoCapacity = cargoCapacity,
            Status = seed.Status,
            ImperialPermitNumber = seed.ImperialPermitNumber,
            Crew = crew
        };
        
        //Maps the cargo datas to Cargo objects
        foreach (var variabCargo in seed.Cargoes)
        {
            var cargo = new Cargo
            {
                CargoType = variabCargo.CargoType,
                Quantity = variabCargo.Quantity,
                ImperialCredits = variabCargo.ImperialCredits,
                Insurance = variabCargo.Insurance,
                RiskLevel = variabCargo.RiskLevel,
            };
            
            //Adds the current Cargo object to the Shipment object
            shipment.Cargoes.Add(cargo);
        }
        
        //returns the mapped Shipment object
        return shipment;
    }
}