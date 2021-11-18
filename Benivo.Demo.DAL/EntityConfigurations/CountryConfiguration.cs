using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class CountryConfiguration : EntityConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            base.Configure(builder);

            builder.ToTable("Countries");

            builder.HasKey(c => c.Id)
                .IsClustered(true)
                .HasName("PK_Countries_Id");

            builder.HasIndex(c => c.Name)
                .IsUnique(true)
                .HasDatabaseName("UK_Countries_Name");

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired(true);
        }
    }
}
