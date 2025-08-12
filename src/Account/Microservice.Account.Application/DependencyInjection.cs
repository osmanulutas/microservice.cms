using FluentValidation;
using Microservice.Account.Application.Account.Command.AddAccount;
using Microservice.Account.Application.Account.Command.UpdateAccount;
using Microservice.Account.EFCore.Repositories;
using Microservice.Account.SharedKernel.SeedWork;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.Account.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);


            #region [ Repositories ]

            services.AddValidatorsFromAssemblyContaining<AddAccountCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAccountCommand>();
            #endregion

            return services;
        }
    }
}
