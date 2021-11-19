using Benivo.Demo.Api.Extensions;
using Benivo.Demo.Api.Infrastructure;
using Benivo.Demo.Common.Models;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Benivo.Demo.Api.Middlewares
{
    public class ExceptionHandleMiddleware : ApplicationMiddleware
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
                Log.Error(ex, ex.Message);
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
