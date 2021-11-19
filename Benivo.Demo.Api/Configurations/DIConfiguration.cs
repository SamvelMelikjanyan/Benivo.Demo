using Benivo.Demo.Api.Infrastructure;
using Benivo.Demo.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Benivo.Demo.Api.Configurations
{
    internal static class DIConfiguration
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureServices(configuration);

            services.AddScoped<ServiceFactory>();
        }
    }
}
