using Benivo.Demo.Api.Constants;
using Benivo.Demo.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Benivo.Demo.Api.Configurations
{
    internal static class AuthConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration.GetSection(AppSettings.UrlSection).GetValue<string>(AppSettings.Authority);

            services.AddAuthentication(Jwt.Bearer)
                .AddJwtBearer(Jwt.Bearer, options =>
                {
                    options.Authority = authority;

                    options.Audience = "benivo.demo";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
        }
    }
}
