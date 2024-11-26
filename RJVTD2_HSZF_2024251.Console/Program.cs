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
                    case "Read Data": System.Console.WriteLine("RD");
                        break;
                }
            }while(selected != "Exit Application");

            System.Console.WriteLine("");
        }
    }
}
