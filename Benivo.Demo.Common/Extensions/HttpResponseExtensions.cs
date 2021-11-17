using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Benivo.Demo.Common.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteResponseAsync<T>(this HttpResponse httpResponse, T response, int statusCode)
        {
            string responseJson = JsonConvert.SerializeObject(response, Formatting.Indented);

            httpResponse.StatusCode = statusCode;
            httpResponse.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

            await httpResponse.WriteAsync(responseJson);
        }
    }
}
