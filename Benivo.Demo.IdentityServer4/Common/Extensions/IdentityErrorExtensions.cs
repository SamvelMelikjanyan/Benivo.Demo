using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Benivo.Demo.IdentityServer4.Common.Extensions
{
    public static class IdentityErrorExtensions
    {
        public static Dictionary<string, string[]> ToDictionary(
            this IEnumerable<IdentityError> identityErrors)
        {
            Dictionary<string, string[]> errors = new();
            List<string> errorCodes = identityErrors?.Select(e => e.Code).Distinct().ToList();

            foreach (var errorCode in errorCodes)
                errors.Add(errorCode, identityErrors.Where(e => e.Code == errorCode).Select(d => d.Description).ToArray());

            return errors;
        }
    }
}
