using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.BLL.Interfaces.Infrastructure;
using Benivo.Demo.BLL.Interfaces.Services;
using Benivo.Demo.BLL.Services;
using Benivo.Demo.Cache;
using Benivo.Demo.Common.Constants;
using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.ThirdPartyServices.Infrastructure;
using Benivo.Demo.ThirdPartyServices.Services;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Benivo.Demo.IoC
{
    public static class DIConfigurationExtension
    {
        private const string _benivoDemoDb = "BenivoDemoDb";
        private const string _benivoIdentity = "BenivoIdentity";
        private const string _clientId = "ClientId";
        private const string _clientSecret = "ClientSecret";
        private const string _scope = "Scope";
        private const string _identityBaseAddress = "IdentityBaseAddress";

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(_benivoDemoDb);

            services.AddDbContext<BenivoDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            var dbContext = services.BuildServiceProvider().GetService<BenivoDbContext>();
            dbContext.Database.Migrate();

            services.AddHttpClient();

            services.AddScoped(services =>
            {
                return CacheHelper.GetOrSetAsync(CacheKeys.BenivoIdentityInfo, async () =>
                {

                    var benivoIdentitySection = configuration.GetSection(_benivoIdentity);

                    var benivoIdentityInfo = new BenivoIdentityInfo()
                    {
                        BaseAddress = benivoIdentitySection.GetValue<string>(_identityBaseAddress),
                        ClientId = benivoIdentitySection.GetValue<string>(_clientId),
                        ClientSecret = benivoIdentitySection.GetValue<string>(_clientSecret),
                        Scope = benivoIdentitySection.GetValue<string>(_scope)
                    };

                    var client = services.GetRequiredService<IHttpClientFactory>().CreateClient();

                    var authority = configuration.GetSection(AppSettings.UrlSection).GetValue<string>(AppSettings.Authority);

                    benivoIdentityInfo.DiscoveryDocument = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                    {
                        Address = authority
                    });

                    return benivoIdentityInfo;
                }, TimeSpan.FromMinutes(5)).GetAwaiter().GetResult();
            });

            //services
            services.AddScoped<IService, Service>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJobAnnouncementService, JobAnnouncementService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();

            //third party services
            services.AddScoped<BenivoIdentityService>();
        }
    }
}
