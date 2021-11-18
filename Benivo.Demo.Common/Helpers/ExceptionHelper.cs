using Benivo.Demo.Common.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace Benivo.Demo.Common.Helpers
{
    public class ExceptionHelper
    {
        public static FaultException<ErrorModel> CreateFaultException(
            string message,
            int statuCode,
            Dictionary<string, string[]> errors = null)
            => new(new()
            {
                Message = message,
                StatusCode = statuCode,
                Errors = errors
            });
    }
}
