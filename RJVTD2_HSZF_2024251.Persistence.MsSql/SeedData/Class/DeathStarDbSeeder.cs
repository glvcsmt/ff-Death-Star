using RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData.Interface;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData.Class;

//Seeder class for the database, to fill it up with base data
public class DeathStarDbSeeder 
{
    //Makes an issue of the neccesseary classes and interface
    private readonly DeathStarDbContext _context;
    private readonly IShipmentDataLoader _dataLoader;
    private readonly ShipmentMapper _mapper;

    //Constructor 
    public DeathStarDbSeeder(DeathStarDbContext context, IShipmentDataLoader dataLoader, ShipmentMapper mapper)
    {
        _context = context;
        _dataLoader = dataLoader;
        _mapper = mapper;
    }
    
    //The actual method that does the seeding
    public void Seed(string jsonFilePath)
    {
        //Checks if the database exists,
        //if yes then it's not filling it up again to prevent duplication
        if(!_context.Database.EnsureCreated()) return;
        
        var shipmentsData = _dataLoader.LoadShipments(jsonFilePath);

        if (shipmentsData != null)
        {
            foreach (var varialShipment in shipmentsData)
            {
                var shipment = _mapper.Map(varialShipment);
                _context.Add(shipment);
            }
        }

        _context.SaveChanges();
    }   
}