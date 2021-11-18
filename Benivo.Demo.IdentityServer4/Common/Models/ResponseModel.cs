using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Benivo.Demo.IdentityServer4.Common.Models
{
    public class ResponseModel<TData> : OkResult
    {
        public string Message { get; set; }

        public TData Data { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }

        /// <summary>
        /// Hide from response body
        /// </summary>
        public new int StatusCode { private get; set; }
    }
}
