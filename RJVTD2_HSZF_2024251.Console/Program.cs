using Microsoft.Extensions.DependencyInjection;
using RJVTD2_HSZF_2024251.Console.UI;
using Microsoft.Extensions.Hosting;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Model;
using RJVTD2_HSZF_2024251.Persistence.MsSql;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;
using Spectre.Console;

namespace RJVTD2_HSZF_2024251.Console
{
    internal class Program
    {
        public static MainUI mainUI;
        static void Main(string[] args)
        {
            var deathStarHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //Context
                    services.AddScoped<DeathStarDbContext>();
                    //Providers
                    services.AddSingleton<ICargoCapacityDataProvider, CargoCapacityDataProvider>();
                    services.AddSingleton<ICargoDataProvider, CargoDataProvider>();
                    services.AddSingleton<ICrewDataProvider, CrewDataProvider>();
                    services.AddSingleton<IDirectoryProvider, DirectoryProvider>();
                    services.AddSingleton<IShipmentDataProvider ,ShipmentDataProvider>();
                    //Services
                    services.AddSingleton<ICargoCapacityService, CargoCapacityService>();
                    services.AddSingleton<ICargoService, CargoService>();
                    services.AddSingleton<ICrewService, CrewService>();
                    services.AddSingleton<IDirectoryService, DirectoryService>();
                    services.AddSingleton<IShipmentService, ShipmentService>();
                })
                .Build();

            deathStarHost.Start();
            
            IServiceScope serviceScope = deathStarHost.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            ICargoCapacityService cargoCapacityService = serviceProvider.GetRequiredService<ICargoCapacityService>();
            ICargoService cargoService = serviceProvider.GetRequiredService<ICargoService>();
            ICrewService crewService = serviceProvider.GetRequiredService<ICrewService>();
            IDirectoryService directoryService = serviceProvider.GetRequiredService<IDirectoryService>();
            IShipmentService shipmentService = serviceProvider.GetRequiredService<IShipmentService>();
            
            System.Console.Clear();
            
            CargoUI cargoUI = new CargoUI(cargoService);
            ShipmentUI shipmentUI = new ShipmentUI(shipmentService, crewService, cargoCapacityService, directoryService);
            CrewUI crewUI = new CrewUI(crewService);
            CargoCapacityUI cargoCapacityUI = new CargoCapacityUI(cargoCapacityService);
            
            mainUI = new MainUI(shipmentUI, cargoUI, crewUI, cargoCapacityUI);

            // Subscribe to events
            //Shipment events
            shipmentUI.ShipmentCreated += (message) => NotifyUser(message);
            shipmentUI.ShipmentUpdated += (message) => NotifyUser(message);
            shipmentUI.ShipmentDeleted += (message) => NotifyUser(message);
            shipmentUI.ShipmentLate += (message) => NotifyUser(message);
            
            //Cargo events
            cargoUI.CargoCreated += (message) => NotifyUser(message);
            cargoUI.CargoUpdated += (message) => NotifyUser(message);
            cargoUI.CargoDeleted += (message) => NotifyUser(message);
            
            string selected;
            do
            {
                AnsiConsole.Write(
                    new FigletText("Death Star Menu")
                        .LeftJustified()
                        .Color(Color.Yellow));
                
                selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[yellow]Select an option from the menu:[/]")
                    .PageSize(10)
                    .MoreChoicesText("[orange](Move up and down to reveal more options!)[/]")
                    .AddChoices(new[]
                    {
                        "View Tables", "Read Data", "Upload Data", "Update Data", "Delete Data", "Generate Report", "Exit Application"
                    }));

                switch (selected)
                {
                    case "View Tables" : ViewTableMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Read Data": ReadMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Upload Data": UploadMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Update Data": UpdateMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Delete Data": DeleteMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                }
            }while(selected != "Exit Application");
            
            System.Console.WriteLine("");
        }
        
        private static void NotifyUser(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]{message}[/]");
        }

        #region MenuAccessories
        
        private static void ReadMenu()
        {
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Which type of data would you like to see?[/]\n")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Shipment", "Cargo", "Back to Main Menu"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.GetShipmentById();
                    break;
                case "Cargo": mainUI.CargoUI.GetCargoById();
                    break;
                case "Back to Main Menu":
                    break;
            }
        }
        
        private static void UploadMenu()
        {
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Which type of data would you like to update?[/]\n")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Shipment", "Cargo", "Back to Main Menu"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.CreateShipment();
                    break;
                case "Cargo": mainUI.CargoUI.CreateCargo();
                    break;
                case "Back to Main Menu":
                    break;
            }
        }
        
        private static void UpdateMenu()
        {
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Which type of data would you like to update?[/]\n")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Shipment", "Cargo", "Back to Main Menu"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.UpdateShipment();
                    break;
                case "Cargo": mainUI.CargoUI.UpdateCargo();
                    break;
                case "Back to Main Menu":
                    break;
            }
        }
        
        private static void DeleteMenu()
        {
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Which type of data would you like to delete?[/]\n")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Shipment", "Cargo", "Back to Main Menu"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.DeleteShipment();
                    break;
                case "Cargo": mainUI.CargoUI.DeleteCargo();
                    break;
                case "Back to Main Menu":
                    break;
            }
        }
        
        private static void ViewTableMenu()
        {
            System.Console.Clear();
            string selected;
            do
            {
                AnsiConsole.Write(
                    new FigletText("Death Star Menu")
                        .LeftJustified()
                        .Color(Color.Yellow));
                
                selected = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Which table would you like to view?[/]\n")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Shipments", "Cargos", "Crews", "Cargo Capacities", "Back to Main Menu"
                        }));

                switch (selected)
                {
                    case "Shipments": DisplayShipments();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Cargos": DisplayCargos();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Crews": DisplayCrews();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Cargo Capacities": DisplayCargoCapacities();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Back to Main Menu":
                        break;
                }
            } while (selected != "Back to Main Menu");
        }
        
        private static void DisplayShipments()
        {
            var shipments = mainUI.ShipmentUI.ReadAllShipments();

            var table = new Table();
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[darkorange3]Ship Type[/]");
            table.AddColumn("[yellow]Shipment Date[/]");
            table.AddColumn("[darkorange3]Status[/]");
            table.AddColumn("[yellow]Imperial Permit Number[/]");

            foreach (var shipment in shipments)
            {
                table.AddRow(shipment.Id, shipment.ShipType, 
                    shipment.ShipmentDate?.ToString("yyyy-MM-dd") ?? "N/A", 
                    shipment.Status, 
                    shipment.ImperialPermitNumber);
            }

            AnsiConsole.Write(table);
        }
        
        private static void DisplayCargos()
        {
            var cargos = mainUI.CargoUI.ReadAllCargoes();

            var table = new Table();
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[darkorange3]Cargo Type[/]");
            table.AddColumn("[yellow]Quantity[/]");
            table.AddColumn("[darkorange3]Imperial Credits[/]");
            table.AddColumn("[yellow]Insurance[/]");
            table.AddColumn("[darkorange3]Risk Level[/]");
            table.AddColumn("[yellow]Shipment ID[/]");

            foreach (var cargo in cargos)
            {
                table.AddRow(cargo.Id, cargo.CargoType, 
                    cargo.Quantity.ToString(), 
                    cargo.ImperialCredits.ToString(), 
                    cargo.Insurance.ToString(), 
                    Enum.GetName(typeof(RiskLevel), cargo.RiskLevel) ?? "Unknown", 
                    cargo.ShipmentId);
            }

            AnsiConsole.Write(table);
        }
        
        private static void DisplayCrews()
        {
            var crews = mainUI.CrewUI.ReadAllCrews();

            var table = new Table();
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[darkorange3]Captain Name[/]");
            table.AddColumn("[yellow]Crew Count[/]");
            table.AddColumn("[darkorange3]Shipment ID[/]");

            foreach (var crew in crews)
            {
                table.AddRow(crew.Id, crew.CaptainName, 
                    crew.CrewCount.ToString(), 
                    crew.ShipmentId);
            }

            AnsiConsole.Write(table);
        }
        
        private static void DisplayCargoCapacities()
        {
            var capacities = mainUI.CargoCapacityUI.ReadAllCargoCapacities();

            var table = new Table();
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[darkorange3]Unit[/]");
            table.AddColumn("[yellow]Amount[/]");
            table.AddColumn("[darkorange3]Shipment ID[/]");

            foreach (var capacity in capacities)
            {
                table.AddRow(capacity.Id, capacity.Unit, 
                    capacity.Amount.ToString(), 
                    capacity.ShipmentId);
            }

            AnsiConsole.Write(table);
        }
        
        #endregion
    }
}
