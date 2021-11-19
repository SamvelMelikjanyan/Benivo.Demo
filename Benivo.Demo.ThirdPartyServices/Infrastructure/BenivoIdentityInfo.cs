using IdentityModel.Client;

namespace Benivo.Demo.ThirdPartyServices.Infrastructure
{
    public class BenivoIdentityInfo
    {
        public string BaseAddress { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }

        public DiscoveryDocumentResponse DiscoveryDocument { get; set; }
    }
}
