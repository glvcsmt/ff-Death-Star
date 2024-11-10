using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RJVTD2_HSZF_2024251.Model;

public enum RiskLevel //Enum to limit RiskLevel to predefined values
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

public class Cargo  //Represents an individual cargo items 
{
    public Cargo(string cargoType, int quantity, int imperialCredits, bool insurance, RiskLevel riskLevel, string shipmentId)
    {
        CargoType = cargoType;
        Quantity = quantity;
        ImperialCredits = imperialCredits;
        Insurance = insurance;
        RiskLevel = riskLevel;
        ShipmentId = shipmentId;
    }

    public Cargo()
    {
        
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } //Primary key for the Cargo
    
    [StringLength(50)]
    [Required]
    public string CargoType { get; set; }   //The name of the cargo type
    
    [Required]
    public int Quantity { get; set; } //The quantity of the shipped cargo type
    
    [Required]
    public int ImperialCredits { get; set; }    //The value of the cargo in Imperial Credits
    
    [Required]
    public bool Insurance { get; set; } //Does the cargo have insurance? 
    
    [Required]
    public RiskLevel RiskLevel { get; set; } //The risk level of the cargo
    
    public string ShipmentId { get; set; } //Foreign key to Shipment
    
    [Required]
    public virtual Shipment Shipment { get; set; }  //Navigation property to Shipment
}