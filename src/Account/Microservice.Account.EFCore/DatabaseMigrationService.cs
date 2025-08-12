using Microservice.Account.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microservice.Account.EFCore
{
    public class DatabaseMigrationService(IServiceProvider serviceProvider, ILogger<DatabaseMigrationService> logger) : IHostedService
    {

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MicroserviceAccountWriteContext>();
            logger.LogInformation("Applying database migrations...");
            await context.Database.MigrateAsync(cancellationToken);
            logger.LogInformation("Database migration applied successfully.");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
