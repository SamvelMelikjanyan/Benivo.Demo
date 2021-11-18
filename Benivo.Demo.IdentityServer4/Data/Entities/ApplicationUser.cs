using Microsoft.AspNetCore.Identity;

namespace Benivo.Demo.IdentityServer4.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public long ExternalId { get; set; }
    }
}
