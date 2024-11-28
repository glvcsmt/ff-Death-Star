using Microsoft.Extensions.DependencyInjection;
using RJVTD2_HSZF_2024251.Console.UI;
using Microsoft.Extensions.Hosting;
using RJVTD2_HSZF_2024251.Application.Services;
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
            
            mainUI = new MainUI(shipmentUI, cargoUI);

            string selected;
            do
            {
                
                selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[yellow]Death Star Menu[/]")
                    .PageSize(10)
                    .MoreChoicesText("[orange](Move up and down to reveal more options!)[/]")
                    .AddChoices(new[]
                    {
                        "Read Data", "Upload Data", "Update Data", "Delete Data", "Generate Report", "Exit Application"
                    }));

                switch (selected)
                {
                    case "Read Data": ReadMenu();
                        break;
                    case "Upload Data": UploadMenu();
                        break;
                    case "Update Data": UpdateMenu();
                        break;
                    case "Delete Data": DeleteMenu();
                        break;
                }
            }while(selected != "Exit Application");

            System.Console.WriteLine("");
        }

        private static void ReadMenu()
        {
            string selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Which type of data would you like to see?[/]\n")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Shipment", "Cargo"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.GetShipmentById();
                    break;
                case "Cargo": mainUI.CargoUI.GetCargoById();
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
                        "Shipment", "Cargo"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.CreateShipment();
                    break;
                case "Cargo": mainUI.CargoUI.CreateCargo();
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
                        "Shipment", "Cargo"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.UpdateShipment();
                    break;
                case "Cargo": mainUI.CargoUI.UpdateCargo();
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
                        "Shipment", "Cargo"
                    }));

            switch (selected)
            {
                case "Shipment": mainUI.ShipmentUI.DeleteShipment();
                    break;
                case "Cargo": mainUI.CargoUI.DeleteCargo();
                    break;
            }
        }
    }
}
