namespace RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData;

//Seeder class for the database, to fill it up with base data
public class DeathStarDbSeeder 
{
    //Makes an issue of the neccesseary classes and interface
    private readonly DeathStarDbContext _context;
    private readonly ShipmentDataLoader _dataLoader;
    private readonly ShipmentMapper _mapper;

    //Constructor 
    public DeathStarDbSeeder(DeathStarDbContext context, string jsonFilePath)
    {
        _context = context;
        _dataLoader = new ShipmentDataLoader();
        _mapper = new ShipmentMapper();
        SeedDatabase(jsonFilePath);
    }
    
    //The actual method that does the seeding
    private void SeedDatabase(string jsonFilePath)
    {

        //Checks if the database exists,
        //if yes then it's not filling it up again to prevent duplication
        if(!(_context.Shipments.Count() == 0)) return;
        
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