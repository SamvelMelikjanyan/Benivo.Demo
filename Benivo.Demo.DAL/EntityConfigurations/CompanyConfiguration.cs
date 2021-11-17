using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class CompanyConfiguration : EntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);

            builder.ToTable("Companies");

            builder.HasKey(c => c.Id)
                .IsClustered(true)
                .HasName("PK_Companies_Id");

            builder.HasIndex(c => c.Name)
                .IsUnique(true)
                .HasDatabaseName("UK_Companies_Name");

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(c => c.Description)
                .HasMaxLength(2000)
                .IsRequired(false);
        }
    }
}
