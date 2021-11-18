using Benivo.Demo.IdentityServer4.Common.Extensions;
using Benivo.Demo.IdentityServer4.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Benivo.Demo.IdentityServer4.Filters
{
    internal class ModelValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ModelStateDictionary modelState = context.ModelState;

            if (!modelState.IsValid)
            {
                var errorResponse = ErrorResponse(context.ModelState);
                await context.HttpContext.Response.WriteResponseAsync(errorResponse, StatusCodes.Status400BadRequest);
                return;
            }

            await next();
        }

        private static ResponseModel<object> ErrorResponse(ModelStateDictionary modelState)
        {
            Dictionary<string, string[]> errors = modelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(k => k.Key, v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            return new ResponseModel<object>()
            {
                Message = "Bad request",
                Errors = errors
            };
        }
    }
}
