using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RJVTD2_HSZF_2024251.Model;

public class Shipment   //Represents the detail information of a shipment
{
    //Constructor
    public Shipment(string shipType, DateTime shipmentDate, CargoCapacity cargoCapacity, string status, string imperialPermitNumber, Crew crew)
    {
        ShipType = shipType;
        ShipmentDate = shipmentDate;
        CargoCapacity = cargoCapacity;
        Status = status;
        ImperialPermitNumber = imperialPermitNumber;
        Crew = crew;
        Cargoes = new HashSet<Cargo>();
    }
    
    //Empty Constructor
    public Shipment()
    {
        Cargoes = new HashSet<Cargo>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }     //Primary key for the database and the Shipment
    
    [StringLength(100)]
    [Required]
    public string ShipType { get; set; }    //The type of the ship
    
    [Required]
    public DateTime ShipmentDate { get; set; }  //The time of the shipment
    
    [Required]
    public CargoCapacity CargoCapacity { get; set; }    //The cargo capacity of the ship
    
    [StringLength(100)]
    [Required]
    public string Status { get; set; }  //The current status of the shipment
    
    [StringLength(100)]
    [Required]
    public string ImperialPermitNumber { get; set; }    //The Imperial Permit Number of the shipment
    
    [Required]
    public Crew Crew { get; set; }  //The crew of the ship
    
    [Required]
    public ICollection<Cargo> Cargoes { get; set; } //The cargoes that make up the shipment 
}