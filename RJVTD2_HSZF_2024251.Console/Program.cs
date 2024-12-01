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
                    services.AddSingleton<IXMLProvider, XMLProvider>();
                    //Services
                    services.AddSingleton<ICargoCapacityService, CargoCapacityService>();
                    services.AddSingleton<ICargoService, CargoService>();
                    services.AddSingleton<ICrewService, CrewService>();
                    services.AddSingleton<IDirectoryService, DirectoryService>();
                    services.AddSingleton<IShipmentService, ShipmentService>();
                    services.AddSingleton<IXMLService, XMLService>();
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
            IXMLService xmlService = serviceProvider.GetRequiredService<IXMLService>();
            
            System.Console.Clear();
            
            CargoUI cargoUI = new CargoUI(cargoService);
            ShipmentUI shipmentUI = new ShipmentUI(shipmentService, crewService, cargoCapacityService, directoryService);
            CrewUI crewUI = new CrewUI(crewService);
            CargoCapacityUI cargoCapacityUI = new CargoCapacityUI(cargoCapacityService);
            XMLUI xmlUI = new XMLUI(xmlService, shipmentService);
            
            mainUI = new MainUI(shipmentUI, cargoUI, crewUI, cargoCapacityUI, xmlUI);

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
            
            //XMLReport event
            xmlUI.XMLReportCreated += (message) => NotifyUser(message);
            
            string selected;
            do
            {
                var font = FigletFont.Load("starwars.flf");
                
                AnsiConsole.Write(
                    new FigletText(font,"Death Star Menu")
                        .LeftJustified()
                        .Color(Color.Yellow));
                
                var rule = new Rule();
                AnsiConsole.Write(rule);
                
                selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[yellow bold italic]Select an option from the menu:[/]")
                    .PageSize(10)
                    .MoreChoicesText("[orange](Move up and down to reveal more options!)[/]")
                    .AddChoices(new[]
                    {
                        "View Tables", "Statistics", "Search by Cargo Type","Read Data", "Upload Data", "Update Data", "Delete Data", "Generate Reports", "Exit Application"
                    }));

                switch (selected)
                {
                    case "View Tables" : ViewTableMenu();
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
                    case "Statistics": StatisticsMenu();    
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Generate Reports": GenerateReportMenu();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Search by Cargo Type": SearchByCargoType();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                }
            }while(selected != "Exit Application");
            
            System.Console.WriteLine("");
        }
        
        private static void NotifyUser(string message)
        {
            AnsiConsole.MarkupLine($"[bold italic rapidblink yellow]{message}[/]");
        }

        #region MenuAccessories

        private static void GenerateReportMenu()
        {
            DisplayShipments();
            mainUI.XMLUI.CreateXMLReport();
        }
        private static void ReadMenu()
        {
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow bold italic]Which type of data would you like to see?[/]\n")
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
                    .Title("[yellow bold italic]Which type of data would you like to update?[/]\n")
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
                    .Title("[italic bold yellow]Which type of data would you like to update?[/]\n")
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
                    .Title("[italic bold yellow]Which type of data would you like to delete?[/]\n")
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
        #endregion
        
        #region TableViewMenuMethods
        
        private static void ViewTableMenu()
        {
            System.Console.Clear();
            string selected;
            do
            {
                var font = FigletFont.Load("starwars.flf");
                
                AnsiConsole.Write(
                    new FigletText(font, "Death Star Menu")
                        .LeftJustified()
                        .Color(Color.Yellow));

                var rule = new Rule();
                AnsiConsole.Write(rule);
                
                selected = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[italic bold yellow]Which table would you like to view?[/]\n")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Shipments", "Cargoes", "Crews", "Cargo Capacities", "Back to Main Menu"
                        }));

                switch (selected)
                {
                    case "Shipments": DisplayShipments();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Cargoes": DisplayCargos();
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
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]ID[/]");
            table.AddColumn("[bold italic darkorange3]Ship Type[/]");
            table.AddColumn("[bold italic yellow]Shipment Date[/]");
            table.AddColumn("[bold italic darkorange3]Status[/]");
            table.AddColumn("[bold italic yellow]Imperial Permit Number[/]");

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
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]ID[/]");
            table.AddColumn("[bold italic darkorange3]Cargo Type[/]");
            table.AddColumn("[bold italic yellow]Quantity[/]");
            table.AddColumn("[bold italic darkorange3]Imperial Credits[/]");
            table.AddColumn("[bold italic yellow]Insurance[/]");
            table.AddColumn("[bold italic darkorange3]Risk Level[/]");
            table.AddColumn("[bold italic yellow]Shipment ID[/]");

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
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]ID[/]");
            table.AddColumn("[bold italic darkorange3]Captain Name[/]");
            table.AddColumn("[bold italic yellow]Crew Count[/]");
            table.AddColumn("[bold italic darkorange3]Shipment ID[/]");

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
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]ID[/]");
            table.AddColumn("[bold italic darkorange3]Unit[/]");
            table.AddColumn("[bold italic yellow]Amount[/]");
            table.AddColumn("[bold italic darkorange3]Shipment ID[/]");

            foreach (var capacity in capacities)
            {
                table.AddRow(capacity.Id, capacity.Unit, 
                    capacity.Amount.ToString(), 
                    capacity.ShipmentId);
            }

            AnsiConsole.Write(table);
        }
        #endregion
        
        #region ReportMenuMethods

        private static void StatisticsMenu()
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
                        .Title("[yellow bold italic]What kind of statistics are you interested in?[/]\n")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Insured Shipments", "Captain's shipments", "Cargo capacity utilization", "Back to Main Menu"
                        }));

                switch (selected)
                {
                    case "Insured Shipments": DisplayInsuredShipments();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Captain's shipments": DisplayCaptainShipments();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Cargo capacity utilization": DisplayCapacityUtilization();
                        System.Console.ReadKey();
                        System.Console.Clear();
                        break;
                    case "Back to Main Menu":
                        break;
                }
            } while (selected != "Back to Main Menu");
        }
        
        private static void DisplayInsuredShipments()
        {
            var insuredShipments = mainUI.ShipmentUI.ReadAllShipments();

            var table = new Table();
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]Ship Type[/]");
            table.AddColumn("[bold italic darkorange3]ID[/]");
            table.AddColumn("[bold italic yellow]Cargo Type[/]");
            table.AddColumn("[bold italic darkorange3]Imperial Credits[/]");
            table.AddColumn("[bold italic yellow]Insurance[/]");

            foreach (var shipment in insuredShipments)
            {
                var insuredCargoes = shipment.Cargoes.Where(c => c.Insurance).ToList();
        
                foreach (var cargo in insuredCargoes)
                {
                    table.AddRow(shipment.ShipType, shipment.Id, cargo.CargoType, cargo.ImperialCredits.ToString(), 
                        cargo.Insurance.ToString());
                }
            }

            AnsiConsole.Write(table);
        }
        
        private static void DisplayCaptainShipments()
        {
            var shipments = mainUI.ShipmentUI.ReadAllShipments();

            var table = new Table();
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]Captain Name[/]");
            table.AddColumn("[bold italic darkorange3]Shipment ID[/]");
            table.AddColumn("[bold italic yellow]Ship Type[/]");

            foreach (var shipment in shipments)
            {
                if (shipment.Crew != null && shipment.Crew.CaptainName != null)
                {
                    table.AddRow(shipment.Crew.CaptainName, shipment.Id, shipment.ShipType);
                }
            }

            AnsiConsole.Write(table);
        }
        private static void DisplayCapacityUtilization()
        {
            var cargoCapacities = mainUI.CargoCapacityUI.ReadAllCargoCapacities();
            var allShipments = mainUI.ShipmentUI.ReadAllShipments();

            var table = new Table();
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]Ship Name[/]");
            table.AddColumn("[bold italic darkorange3]Arrival Date[/]");
            table.AddColumn("[bold italic yellow]Total Capacity[/]");
            table.AddColumn("[bold italic darkorange3]Used Capacity[/]");
            table.AddColumn("[bold italic yellow]Utilization (%)[/]");

            foreach (var capacity in cargoCapacities)
            {
                var shipment = allShipments.FirstOrDefault(s => s.Id == capacity.ShipmentId);

                if (shipment != null)
                {
                    var totalCargoWeight = shipment.Cargoes.Sum(c => c.Quantity);

                    var utilizationPercentage = (totalCargoWeight / capacity.Amount) * 100;

                    table.AddRow(
                        shipment.ShipType,
                        shipment.ShipmentDate.Value.ToString("yyyy-MM-dd"),
                        capacity.Amount.ToString(),
                        totalCargoWeight.ToString(),
                        $"{utilizationPercentage:F2}%"
                    );
                }
            }

            AnsiConsole.Write(table);
        }
        #endregion
        
        #region CargoSearch
        private static void SearchByCargoType()
        {
            string cargoType = Commands.GetString("Enter Cargo Type: ");
            var allCargos = mainUI.CargoUI.ReadAllCargoes();
            var filteredCargos = allCargos.Where(c => c.CargoType == cargoType);

            var table = new Table();
            table.Border = TableBorder.Double;
            table.AddColumn("[bold italic yellow]Ship type[/]");
            table.AddColumn("[bold italic darkorange3]Arrival date[/]");
            table.AddColumn("[bold italic yellow]Cargo type[/]");
            table.AddColumn("[bold italic darkorange3]Amount[/]");

            foreach (var cargo in filteredCargos)
            {
                table.AddRow(
                    cargo.Shipment.ShipType,
                    cargo.Shipment.ShipmentDate.Value.ToString("yyyy-MM-dd"),
                    cargo.CargoType,
                    cargo.Quantity.ToString()
                );
            }

            AnsiConsole.Write(table);
        }
        #endregion
    }
}
