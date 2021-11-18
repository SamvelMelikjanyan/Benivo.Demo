using Benivo.Demo.IdentityServer4.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Benivo.Demo.IdentityServer4.Configurations
{
    internal static class FiltersConfiguration
    {
        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.Filters.Add(new ModelValidationFilter());
            });
        }
    }
}
