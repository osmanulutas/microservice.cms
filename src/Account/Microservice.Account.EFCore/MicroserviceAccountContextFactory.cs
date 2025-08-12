using Microservice.Account.EFCore.Context;
using Microservice.Account.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Microservice.Account.EFCore
{
    public class MicroserviceAccountContextFactory : IDesignTimeDbContextFactory<MicroserviceAccountWriteContext>
    {
        public MicroserviceAccountWriteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MicroserviceAccountWriteContext>();

            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine(basePath);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(basePath, "../Microservice.Account.API"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            Console.WriteLine(Path.Combine(basePath, "../../Microservice.Account.API"));
            var connectionString = configuration.GetSecretValue<string>("BuybaseAdminWriteDatabase");
            optionsBuilder.UseNpgsql(connectionString);

            return new MicroserviceAccountWriteContext(optionsBuilder.Options, null, configuration);
        }
    }
}
