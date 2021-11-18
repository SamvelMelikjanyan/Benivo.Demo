using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Benivo.Demo.DAL.Infrastructure
{
    public partial class BenivoDbContext : DbContext
    {
        public BenivoDbContext(DbContextOptions<BenivoDbContext> options)
           : base(options)
        { }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<JobCategory> JobCategories { get; set; }

        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

        public virtual DbSet<JobAnnouncement> JobAnnouncements { get; set; }
    
        public virtual DbSet<User> Users { get; set; }
    
        public virtual DbSet<UserJobAnnouncement> UserJobAnnouncements { get; set; }
    }
}
