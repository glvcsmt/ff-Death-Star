using Microsoft.EntityFrameworkCore;
using RJVTD2_HSZF_2024251.Model;

namespace RJVTD2_HSZF_2024251.Persistence.MsSql;

public class DeathStarDbContext : DbContext //DbContext class that contains the DbSets for the database tables
{
    public DbSet<Shipment> Shipments { get; set; }  //DbSet for the table that contains the Shipments
    
    public DbSet<Crew> Crews { get; set; }  //DbSet for the table that contains the Crews
    
    public DbSet<CargoCapacity> CargoCapacities { get; set; } //DbSet for the table that contains the CargoCapacities
    
    public DbSet<Cargo> Cargoes { get; set; }    //DbSet for the table that contains the Cargoes

    public DeathStarDbContext()
    {
        this.Database.EnsureCreated(); //This ensures that the database is created
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=deathstardb;Integrated Security=True;MultipleActiveResultSets=true";
        optionsBuilder.UseSqlServer(connStr);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}