using Microservice.Content.EFCore.Context;
using Microservice.Content.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Microservice.Content.EFCore
{
    public class MicroserviceContentContextFactory : IDesignTimeDbContextFactory<MicroserviceContentWriteContext>
    {
        public MicroserviceContentWriteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MicroserviceContentWriteContext>();

            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine(basePath);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(basePath, "../Microservice.Content.API"))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            Console.WriteLine(Path.Combine(basePath, "../../Microservice.Content.API"));
            var connectionString = configuration.GetSecretValue<string>("MicroserviceContentWriteContext");
            optionsBuilder.UseNpgsql(connectionString);

            return new MicroserviceContentWriteContext(optionsBuilder.Options, null, configuration);
        }
    }
}
