using Microsoft.Extensions.Configuration;
namespace Microservice.Account.SharedKernel.Utils
{
    public static class ConfigurationExtensions
    {
        public static T GetSecretValue<T>(this IConfiguration configuration, string key)
        {
            return (T)Convert.ChangeType(configuration?.GetSection("AppSettings")[key], typeof(T));
        }
    }
}
