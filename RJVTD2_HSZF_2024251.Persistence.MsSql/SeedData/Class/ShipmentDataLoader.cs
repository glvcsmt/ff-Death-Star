using Newtonsoft.Json;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData.Interface;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData.Class;

//Class that implements the IShipmentDataLoader interface
//and then converts the given file to an actual JSON object which it returns.
public class ShipmentDataLoader : IShipmentDataLoader
{
    public List<Shipment>? LoadShipments(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Shipment>>(json);
    }
}