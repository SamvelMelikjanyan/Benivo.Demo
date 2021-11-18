using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Benivo.Demo.IdentityServer4.Common.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteResponseAsync<T>(this HttpResponse httpResponse, T response, int statucCode)
        {
            string responseJson = JsonConvert.SerializeObject(response, Formatting.Indented);
            httpResponse.StatusCode = statucCode;
            await httpResponse.WriteAsync(responseJson);
        }
    }
}
