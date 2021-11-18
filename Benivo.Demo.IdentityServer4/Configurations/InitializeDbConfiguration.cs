using Benivo.Demo.IdentityServer4.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Benivo.Demo.IdentityServer4.Configurations
{
    internal static partial class InitializeDbConfiguration
    {
        public static async Task InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            await serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

            var benivoDbContext = serviceScope.ServiceProvider.GetRequiredService<BenivoIdentityDbContext>();
            await benivoDbContext.Database.MigrateAsync();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            await context.Database.MigrateAsync();

            if (!await context.Clients.AnyAsync())
            {
                foreach (var client in Clients)
                    await context.Clients.AddAsync(client.ToEntity());

                await context.SaveChangesAsync();
            }

            if (!await context.IdentityResources.AnyAsync())
            {
                foreach (var resource in IdentityResources)
                    await context.IdentityResources.AddAsync(resource.ToEntity());

                await context.SaveChangesAsync();
            }

            if (!await context.ApiScopes.AnyAsync())
            {
                foreach (var scope in ApiScopes)
                    await context.ApiScopes.AddAsync(scope.ToEntity());

                await context.SaveChangesAsync();
            }

            if (!await context.ApiResources.AnyAsync())
            {
                foreach (var resource in ApiResources)
                    await context.ApiResources.AddAsync(resource.ToEntity());

                await context.SaveChangesAsync();
            }
        }
    }
}
