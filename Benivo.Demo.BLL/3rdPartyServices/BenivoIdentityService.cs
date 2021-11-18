using Benivo.Demo.BLL.Interfaces._3rdPartyServices;
using Benivo.Demo.Common.Constants;
using Benivo.Demo.Common.Helpers;
using Benivo.Demo.Models.Infrastructure;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL._3rdPartyServices
{
    public class BenivoIdentityService : IBenivoIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly Dictionary<string, string> _headers;
        private readonly string _baseAddress;

        public BenivoIdentityService(IConfiguration configuration)
        {
            _configuration = configuration;
            _discoveryDocument = GetDiscoveryDocumentAsync().GetAwaiter().GetResult();
            _baseAddress = configuration.GetSection(AppSettings.BenivoIdentity).GetValue<string>(AppSettings.IdentityBaseAddress);
            _headers = FillHeaders().GetAwaiter().GetResult();
        }

        public async Task<int> RegisterUser(string username, string email, string password, long id)
        {
            Dictionary<string, object> body = new()
            {
                { "Username", username },
                { "Email", email },
                { "Password", password },
                { "ExternalId", id }
            };

            RequestInfo requestInfo = new(_baseAddress, "Account/Register", Method.POST, body, _headers);

            IRestResponse<IdentityResponse<int?>> identityResponse =
                await HttpHelper.PostAsync<IdentityResponse<int?>>(requestInfo);

            if (identityResponse.StatusCode != HttpStatusCode.OK)
                throw ExceptionHelper.CreateFaultException(
                    identityResponse.Data?.Message,
                    (int)identityResponse.StatusCode,
                    identityResponse.Data?.Errors); ;

            return identityResponse.Data.Data ?? 0;
        }

        #region private methods
        private async Task<Dictionary<string, string>> FillHeaders()
        {
            var tokenResponse = await ClientCredentialsTokenAsync();

            return new Dictionary<string, string>()
            {
                { "Authorization", $"Bearer {tokenResponse.AccessToken}" },
            };
        }

        private async Task<TokenResponse> ClientCredentialsTokenAsync()
        {
            HttpClient client = new();

            var benivoIdentitySection = _configuration.GetSection(AppSettings.BenivoIdentity);

            var clientId = benivoIdentitySection.GetValue<string>(AppSettings.ClientId);
            var clientSecret = benivoIdentitySection.GetValue<string>(AppSettings.ClientSecret);
            var scope = benivoIdentitySection.GetValue<string>(AppSettings.Scope);

            return await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    Scope = scope
                });
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync()
        {
            HttpClient client = new();

            var authority = _configuration.GetSection(AppSettings.UrlSection)
                .GetValue<string>(AppSettings.Authority);

            return await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = authority
            });
        }
        #endregion
    }
}
