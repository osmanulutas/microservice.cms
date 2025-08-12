using Microservice.Content.EFCore.Context;
using Microservice.Content.EFCore.Repositories;
using Microservice.Content.SharedKernel.SeedWork;
using Microservice.Content.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Content.EFCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MicroserviceContentReadContext>(opt => opt.UseNpgsql(configuration.GetSecretValue<string>("MicroserviceContentReadContext")));
            services.AddDbContext<MicroserviceContentWriteContext>(opt => opt.UseNpgsql(configuration.GetSecretValue<string>("MicroserviceContentWriteContext")));

            //Generic Repository Registration
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddHostedService<DatabaseMigrationService>();
            
            return services;
        }
    }
}
