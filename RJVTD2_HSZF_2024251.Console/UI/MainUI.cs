namespace RJVTD2_HSZF_2024251.Console.UI;

public class MainUI
{
    public ShipmentUI ShipmentUI { get; }
    public CargoUI CargoUI { get; }
    
    public MainUI(ShipmentUI shipmentUI, CargoUI cargoUI)
    {
        ShipmentUI = shipmentUI;
        CargoUI = cargoUI;
    }
}