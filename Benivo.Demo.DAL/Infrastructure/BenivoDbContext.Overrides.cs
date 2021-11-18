using Benivo.Demo.Entities.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Benivo.Demo.DAL.Infrastructure
{
    public partial class BenivoDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTimeOnUpdate();
           
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            ChangeTimeOnUpdate();
            
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTimeOnUpdate();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTimeOnUpdate();
          
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ChangeTimeOnUpdate()
        {
            var utcNow = DateTime.UtcNow;
            
            ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList()
               .ForEach(e => ((BaseEntity)e.Entity).LastUpdateTime = utcNow);
        }
    }
}
