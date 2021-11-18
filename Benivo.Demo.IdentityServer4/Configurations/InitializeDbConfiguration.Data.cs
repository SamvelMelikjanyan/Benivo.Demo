using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Benivo.Demo.IdentityServer4.Configurations
{
    internal static partial class InitializeDbConfiguration
    {
        private const string _benivoDemo = "benivo.demo";

        private static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        private static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(LocalApi.ScopeName),
                new ApiScope(_benivoDemo, "Benivo Demo")
            };

        private static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource(LocalApi.ScopeName) { Scopes = new[]{ LocalApi.ScopeName } },
                new ApiResource(_benivoDemo, "Benivo Demo") { Scopes = new[]{ _benivoDemo } }
            };

        private static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "benivo.password",
                    ClientName = "Resource owner password client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = {
                        _benivoDemo,
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.OfflineAccess
                    },

                    AllowOfflineAccess = true
                },

                new Client
                {
                    ClientId = "benivo.client",
                    ClientName = "Client credentials client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("235436ED-E280-1567-80CA-1C81D202F69B".Sha256()) },

                    AllowedScopes = {
                        LocalApi.ScopeName
                    }
                }
            };
    }
}
