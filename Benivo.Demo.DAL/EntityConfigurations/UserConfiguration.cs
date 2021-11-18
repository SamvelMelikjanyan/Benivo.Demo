using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class UserConfiguration : EntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.HasKey(u => u.Id)
                .IsClustered(true)
                .HasName("PK_Users_Id");

            builder.HasIndex(u => u.Username)
                .IsUnique(true)
                .HasDatabaseName("UK_Users_Username");

            builder.HasIndex(u => u.Email)
                .IsUnique(true)
                .HasDatabaseName("UK_Users_Email");

            builder.Property(u => u.Username)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(u => u.FirstName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired(true)
                .HasMaxLength(50);
        }
    }
}
