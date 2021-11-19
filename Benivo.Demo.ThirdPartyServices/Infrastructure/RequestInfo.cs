using RestSharp;
using System.Collections.Generic;

namespace Benivo.Demo.ThirdPartyServices.Infrastructure
{
    public class RequestInfo
    {
        public RequestInfo(string baseAddress, string endpoint, Method method, Dictionary<string, object> body, Dictionary<string, string> headers)
        {
            Client = new RestClient(baseAddress);
            Request = new RestRequest(endpoint, method);
            Body = body;

            FillRequestHeaders(headers);
        }

        public IRestClient Client { get; private set; }

        public IRestRequest Request { get; private set; }

        public Dictionary<string, object> Body { get; private set; }

        private void FillRequestHeaders(Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
                Request.AddHeader(header.Key, header.Value);
        }
    }
}
