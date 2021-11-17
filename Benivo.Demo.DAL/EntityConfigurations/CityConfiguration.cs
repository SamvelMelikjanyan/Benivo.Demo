using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class CityConfiguration : EntityConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);

            builder.ToTable("Cities");

            builder.HasKey(c => c.Id)
                .IsClustered(true)
                .HasName("PK_Cities_Id");

            builder.HasIndex(c => new { c.Name, c.CountryId })
                .IsUnique()
                .HasDatabaseName("UK_Cities_NameCountryId");

            builder.HasOne(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountryId)
                .HasConstraintName("FK_Cities_Countries_CountryId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired(true);
        }
    }
}
