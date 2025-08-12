using FluentValidation;
using Microservice.Content.Application.Content.Commands.AddContent;
using Microservice.Content.Application.Content.Commands.UpdateContent;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.Content.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

            services.AddValidatorsFromAssemblyContaining<AddContentCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateContentCommandValidator>();

            return services;
        }
    }
}
