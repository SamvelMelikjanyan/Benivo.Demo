using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Benivo.Demo.IdentityServer4.Infrastructure
{
    internal abstract class ApplicationMiddleware
    {
        protected readonly RequestDelegate Next;

        public ApplicationMiddleware(RequestDelegate next) => Next = next;

        public abstract Task InvokeAsync(HttpContext httpContext);
    }
}
