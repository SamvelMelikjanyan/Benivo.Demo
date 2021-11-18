using Benivo.Demo.BLL._3rdPartyServices;
using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.BLL.Interfaces._3rdPartyServices;
using Benivo.Demo.BLL.Interfaces.Infrastructure;
using Benivo.Demo.BLL.Interfaces.Services;
using Benivo.Demo.BLL.Services;
using Benivo.Demo.Common.Constants;
using Benivo.Demo.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Benivo.Demo.IoC
{
    public static class DIConfigurationExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(AppSettings.BenivoDemoDb);

            services.AddDbContext<BenivoDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            var dbContext = services.BuildServiceProvider().GetService<BenivoDbContext>();
            dbContext.Database.Migrate();

            //services
            services.AddScoped<IService, Service>();
            services.AddScoped<IBenivoIdentityService, BenivoIdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJobAnnouncementService, JobAnnouncementService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();
        }
    }
}
