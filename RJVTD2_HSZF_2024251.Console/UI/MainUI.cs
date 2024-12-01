namespace RJVTD2_HSZF_2024251.Console.UI;

// This class acts as a central point for managing and accessing various UI components 
// for different parts of the system such as Shipments, Cargo, Crew, Cargo Capacity, and XML-related tasks.
public class MainUI
{
    // Public properties representing different UI components for various entities in the system.
    public ShipmentUI ShipmentUI { get; }
    public CargoUI CargoUI { get; }
    public CrewUI CrewUI { get; }
    public CargoCapacityUI CargoCapacityUI { get; }
    public XMLUI XMLUI { get; }
    
    // Constructor to initialize the UI components via dependency injection.
    public MainUI(ShipmentUI shipmentUI, CargoUI cargoUI, CrewUI crewUI, CargoCapacityUI cargoCapacityUI, XMLUI xmlUI)
    {
        // Assigning the passed UI components to the respective properties of MainUI.
        ShipmentUI = shipmentUI;
        CargoUI = cargoUI;
        CrewUI = crewUI;
        CargoCapacityUI = cargoCapacityUI;
        XMLUI = xmlUI;
    }
}