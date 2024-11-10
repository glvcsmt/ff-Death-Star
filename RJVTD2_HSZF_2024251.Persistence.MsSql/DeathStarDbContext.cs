using Microsoft.EntityFrameworkCore;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql;

public class DeathStarDbContext : DbContext //DbContext class that contains the DbSets for the database tables
{
    public DbSet<Shipment> Shipments { get; set; }  //DbSet for the table that contains the Shipments
    
    public DbSet<Crew> Crews { get; set; }  //DbSet for the table that contains the Crews
    
    public DbSet<CargoCapacity> CargoCapacities { get; set; } //DbSet for the table that contains the CargoCapacities
    
    public DbSet<Cargo> Cargoes { get; set; }    //DbSet for the table that contains the Cargoes

    //Constructor
    public DeathStarDbContext()
    {
        this.Database.EnsureCreated(); //This ensures that the database is created
    }
    
    //Database configuration method
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Database connecting path which contains the path on which the program can connect to the database 
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=deathstardb;Integrated Security=True;MultipleActiveResultSets=true";
        optionsBuilder.UseSqlServer(connStr).UseLazyLoadingProxies();   //Enables Lazy Loading Proxies
        base.OnConfiguring(optionsBuilder);
    }
}