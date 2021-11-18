using Benivo.Demo.Api.Infrastructure;
using Benivo.Demo.Common.Extensions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Benivo.Demo.Api.Middlewares
{
    internal class UnauthorizedResponseHandleMiddleware : ApplicationMiddleware
    {
        public UnauthorizedResponseHandleMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            await Next(httpContext);
            
            if (!IsAuthorized(httpContext.Response, out ResponseModel<string> response))
                await httpContext.Response.WriteResponseAsync(response, httpContext.Response.StatusCode);
        }

        private static bool IsAuthorized(HttpResponse httpResponse, out ResponseModel<string> response)
        {
            response = null;

            if (httpResponse.StatusCode == StatusCodes.Status401Unauthorized)
            {
                response = new ResponseModel<string>()
                {
                    Message = "Authorization has been denied for this request"
                };

                return false;
            }

            if (httpResponse.StatusCode == StatusCodes.Status403Forbidden)
            {
                response = new ResponseModel<string>()
                {
                    Message = "Permission denied"
                };

                return false;
            }

            return true;
        }
    }
}
