using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Benivo.Demo.Common.Helpers
{
    public class HttpHelper
    {
        public static async Task<IRestResponse<TResponse>> PostAsync<TResponse>(RequestInfo requestInfo)
        {
            IRestClient client = requestInfo.Client;
            IRestRequest request = requestInfo.Request;

            var cancellationTokenSource = new CancellationTokenSource();

            request.AddJsonBody(requestInfo.Body);

            IRestResponse<TResponse> responseMessage = await client.ExecuteAsync<TResponse>(request, cancellationTokenSource.Token);

            if (!responseMessage.IsSuccessful)
                responseMessage.Data = JsonConvert.DeserializeObject<TResponse>(responseMessage.Content);

            return responseMessage;
        }
    }

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
