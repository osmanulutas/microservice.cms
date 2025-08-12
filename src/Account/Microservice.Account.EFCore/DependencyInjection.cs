using Microservice.Account.EFCore.Context;
using Microservice.Account.EFCore.Repositories;
using Microservice.Account.SharedKernel.SeedWork;
using Microservice.Account.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Account.EFCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MicroserviceAccountReadContext>(opt => opt.UseNpgsql(configuration.GetSecretValue<string>("MicroserviceAccountReadContext")));
            services.AddDbContext<MicroserviceAccountWriteContext>(opt => opt.UseNpgsql(configuration.GetSecretValue<string>("MicroserviceAccountWriteContext")));

            //Generic Repository Registration
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddHostedService<DatabaseMigrationService>();
            
            return services;
        }
    }
}
