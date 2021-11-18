using System.Collections.Generic;

namespace Benivo.Demo.Models.Infrastructure
{
    public class IdentityResponse<TData>
    {
        public string Message { get; set; }

        public TData Data { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
