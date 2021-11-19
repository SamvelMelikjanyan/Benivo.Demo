using Benivo.Demo.Api.Infrastructure;
using Benivo.Demo.Cache;
using Benivo.Demo.IoC;
using Benivo.Demo.ThirdPartyServices.Infrastructure;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

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
