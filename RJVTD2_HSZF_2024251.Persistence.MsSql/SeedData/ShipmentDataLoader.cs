using Newtonsoft.Json;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData;

//Class that converts the given file to an actual JSON object which it returns
public class ShipmentDataLoader
{
    //
    public List<Shipment>? LoadShipments(string filePath)
    {
        //Checking if the given JSON file actually exists
        //If the existance is false then it will write it out to console 
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return null;
        }
        
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Shipment>>(json);
    }
}