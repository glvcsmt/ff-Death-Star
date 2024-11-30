﻿namespace RJVTD2_HSZF_2024251.Console.UI;

public class MainUI
{
    public ShipmentUI ShipmentUI { get; }
    public CargoUI CargoUI { get; }
    
    public CrewUI CrewUI { get; }
    
    public CargoCapacityUI CargoCapacityUI { get; }
    
    public XMLUI XMLUI { get; }
    
    public MainUI(ShipmentUI shipmentUI, CargoUI cargoUI, CrewUI crewUI, CargoCapacityUI cargoCapacityUI, XMLUI xmlUI)
    {
        ShipmentUI = shipmentUI;
        CargoUI = cargoUI;
        CrewUI = crewUI;
        CargoCapacityUI = cargoCapacityUI;
        XMLUI = xmlUI;
    }
}