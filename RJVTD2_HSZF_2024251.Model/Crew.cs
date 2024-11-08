using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RJVTD2_HSZF_2024251.Model;

public class Crew //Represents the crew of the ship
{
    public Crew(string captainName, int crewCount, string shipmentId)
    {
        CaptainName = captainName;
        CrewCount = crewCount;
        ShipmentId = shipmentId;
    }

    public Crew()
    {
        
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } //Primary key for the crew
    
    [StringLength(50)]
    [Required]
    public string CaptainName { get; set; } //The name of the captain of the ship
    
    [Required]
    public int CrewCount { get; set; }  //The number of the crew members on the ship
    
    public string ShipmentId { get; set; } //Foreign key to Shipment
    
    [Required]
    public Shipment Shipment { get; set; }  //Navigation property to Shipment
}