using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class JobTypeConfiguration : EntityConfiguration<JobType>
    {
        public override void Configure(EntityTypeBuilder<JobType> builder)
        {
            base.Configure(builder);

            builder.ToTable("JobTypes");

            builder.HasKey(jt => jt.Id)
                .IsClustered(true)
                .HasName("PK_JobTypes");

            builder.HasIndex(jt => jt.Name)
                .IsUnique(true)
                .HasDatabaseName("UK_JobTypes_Name");

            builder.Property(jt => jt.Name)
                .HasMaxLength(255)
                .IsRequired(true);
        }
    }
}
