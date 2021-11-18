using Benivo.Demo.IdentityServer4.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using static IdentityServer4.IdentityServerConstants;

namespace Benivo.Demo.IdentityServer4.Configurations
{
    internal static class AuthConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration.GetSection(AppSettings.URLSection).GetValue<string>(AppSettings.Authority);

            services.AddAuthentication(LocalApi.AuthenticationScheme)
                .AddJwtBearer(LocalApi.AuthenticationScheme, options =>
                {
                    options.Authority = authority;

                    options.Audience = LocalApi.ScopeName;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
