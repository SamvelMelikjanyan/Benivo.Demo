using Benivo.Demo.Api.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Benivo.Demo.Api.Configurations
{
    internal static class FiltersConfiguration
    {
        public static void ConfigureFilters(this IServiceCollection services)
            => services.AddMvc(option =>
            {
                option.Filters.Add(new ModelValidationFilter());
            });
    }
}
