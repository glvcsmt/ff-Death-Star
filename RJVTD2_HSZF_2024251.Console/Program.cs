using Microsoft.Extensions.DependencyInjection;
using RJVTD2_HSZF_2024251.Console.UI;
using Microsoft.Extensions.Hosting;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Persistence.MsSql;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

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
        }
    }
}
