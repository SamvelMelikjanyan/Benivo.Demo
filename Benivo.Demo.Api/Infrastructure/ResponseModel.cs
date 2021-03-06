using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Benivo.Demo.Api.Infrastructure
{
    public class ResponseModel<TData> : ActionResult
    {
        public string Message { get; set; }

        public TData Data { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
