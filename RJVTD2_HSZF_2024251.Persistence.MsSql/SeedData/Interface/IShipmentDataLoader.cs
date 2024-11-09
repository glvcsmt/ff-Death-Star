using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData.Interface;

//Interface to load a shipment JSON so that the implementing class
//could make it an actual object. 
public interface IShipmentDataLoader
{
    List<Shipment>? LoadShipments(string filePath);
}