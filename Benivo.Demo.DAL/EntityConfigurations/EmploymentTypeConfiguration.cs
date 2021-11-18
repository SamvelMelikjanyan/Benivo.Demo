using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class EmploymentTypeConfiguration : EntityConfiguration<EmploymentType>
    {
        public override void Configure(EntityTypeBuilder<EmploymentType> builder)
        {
            base.Configure(builder);

            builder.ToTable("EmployementTypes");

            builder.HasKey(jt => jt.Id)
                .IsClustered(true)
                .HasName("PK_EmployementTypes");

            builder.Property(jt => jt.Id)
                .UseIdentityColumn();

            builder.HasIndex(jt => jt.Name)
                .IsUnique(true)
                .HasDatabaseName("UK_EmployementTypes_Name");

            builder.Property(jt => jt.Name)
                .HasMaxLength(255)
                .IsRequired(true);
        }
    }
}
