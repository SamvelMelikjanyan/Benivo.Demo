using Benivo.Demo.IdentityServer4.Common.Extensions;
using Benivo.Demo.IdentityServer4.Common.Models;
using Benivo.Demo.IdentityServer4.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Benivo.Demo.IdentityServer4.Middlewares
{
    internal class ExceptionHandleMiddleware : ApplicationMiddleware
    {
        public ExceptionHandleMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch (Exception ex)
            {
                var response = BindErrorResponse(ex, out int statusCode);
                await httpContext.Response.WriteResponseAsync(response, statusCode);
            }
        }

        private static ResponseModel<object> BindErrorResponse(Exception exception, out int statusCode)
        {
            if (exception is FaultException<ErrorModel> faultException)
            {
                statusCode = faultException.Detail.StatusCode;

                return new()
                {
                    Message = faultException.Detail.Message,
                    Errors = faultException.Detail.Errors
                };
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;

                return new()
                {
                    Message = "Something went wrong",
                };
            }
        }
    }
}
