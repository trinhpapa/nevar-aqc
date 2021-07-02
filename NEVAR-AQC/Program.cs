using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NEVAR_AQC.Data.EF;
using System;

namespace NEVAR_AQC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbInitializer = services.GetService<DbInitializer>();
                    dbInitializer.SeedRole().Wait();
                    dbInitializer.SeedDepartment().Wait();
                    dbInitializer.SeedRequirementType().Wait();
                    dbInitializer.SeedReturnInvoiceResultType().Wait();
                    dbInitializer.SeedRequirementStatus().Wait();
                    dbInitializer.SeedUser().Wait();
                    dbInitializer.SeedFunction().Wait();
                    dbInitializer.SeedRoleFunction().Wait();
                    dbInitializer.SeedCustomerType().Wait();
                    dbInitializer.SeedCustomer().Wait();
                    dbInitializer.SeedField().Wait();
                    dbInitializer.SeedTestObject().Wait();
                    dbInitializer.SeedTestProperty().Wait();
                    dbInitializer.SeedTestMethod().Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}