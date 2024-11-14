using RJVTD2_HSZF_2024251.Persistence.MsSql;
using RJVTD2_HSZF_2024251.Persistence.MsSql.SeedData;

namespace RJVTD2_HSZF_2024251.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DeathStarDbContext();
        }
    }
}
