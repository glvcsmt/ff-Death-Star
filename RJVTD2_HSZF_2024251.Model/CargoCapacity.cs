using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RJVTD2_HSZF_2024251.Model;

public class CargoCapacity  //Represents the cargo capacity of the ship
{
    public CargoCapacity(string unit, double amount, string shipmentId)
    {
        Unit = unit;
        Amount = amount;
        ShipmentId = shipmentId;
    }

    public CargoCapacity()
    {
        
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } //Primary key for the cargo capacity
    
    [StringLength(50)]
    [Required]
    public string Unit { get; set; }    //The unit of the carry capacity
    
    [Required]
    public double Amount { get; set; }  //The amount of the carried weight

    public string ShipmentId { get; set; } //Foreign key to Shipment
    
    [Required]
    public virtual Shipment Shipment { get; set; }  //Navigation property to Shipment
}