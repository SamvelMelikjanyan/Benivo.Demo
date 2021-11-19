using Benivo.Demo.Common.Helpers;
using Benivo.Demo.ThirdPartyServices.Helpers;
using Benivo.Demo.ThirdPartyServices.Infrastructure;
using IdentityModel.Client;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Benivo.Demo.ThirdPartyServices.Services
{
    public class BenivoIdentityService
    {
        private readonly BenivoIdentityInfo _benivoIdentityInfo;
        private readonly Dictionary<string, string> _headers;

        public BenivoIdentityService(BenivoIdentityInfo benivoIdentityInfo)
        {
            _benivoIdentityInfo = benivoIdentityInfo;
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

            RequestInfo requestInfo = new(_benivoIdentityInfo.BaseAddress, "Account/Register", Method.POST, body, _headers);

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

            return await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _benivoIdentityInfo.DiscoveryDocument.TokenEndpoint,
                    ClientId = _benivoIdentityInfo.ClientId,
                    ClientSecret = _benivoIdentityInfo.ClientSecret,
                    Scope = _benivoIdentityInfo.Scope
                });
        }

        #endregion
    }
}
