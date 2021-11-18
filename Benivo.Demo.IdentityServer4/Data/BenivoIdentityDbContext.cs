using Benivo.Demo.IdentityServer4.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Benivo.Demo.IdentityServer4.Data
{
    public class BenivoIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public BenivoIdentityDbContext(DbContextOptions<BenivoIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
