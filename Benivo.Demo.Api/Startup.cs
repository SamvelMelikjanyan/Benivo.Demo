using Benivo.Demo.Api.Configurations;
using Benivo.Demo.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Benivo.Demo.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureDI(_configuration);
            services.ConfigureAuthentication(_configuration);
            services.ConfigureAuthorization();
            services.ConfigureApiVersioning();
            services.ConfigureFluentValidation();
            services.ConfigureSwagger();
            services.ConfigureFilters();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseSerilogRequestLogging();

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<UnauthorizedResponseHandleMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
