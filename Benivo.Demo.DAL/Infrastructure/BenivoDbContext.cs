using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Benivo.Demo.DAL.Infrastructure
{
    public class BenivoDbContext : DbContext
    {
        public BenivoDbContext(DbContextOptions<BenivoDbContext> options)
           : base(options)
        { }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<City> Cities { get; set; }
       
        public virtual DbSet<Company> Companies { get; set; }
        
        public virtual DbSet<JobCategory> JobCategories { get; set; }

        public virtual DbSet<JobType> JobTypes { get; set; }
        
        public virtual DbSet<JobAnnouncement> JobAnnouncements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
