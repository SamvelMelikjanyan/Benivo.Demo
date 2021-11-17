using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class JobCategoryConfiguration : EntityConfiguration<JobCategory>
    {
        public override void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            base.Configure(builder);

            builder.ToTable("JobCategories");

            builder.HasKey(jc => jc.Id)
                .IsClustered(true)
                .HasName("PK_JobCategories_Id");

            builder.HasIndex(jc => jc.Name)
                .IsUnique(true)
                .HasDatabaseName("UK_JobCategories_Name");

            builder.HasOne(jc => jc.Parent)
                .WithMany(jc => jc.SubCategories)
                .HasForeignKey(jc => jc.ParentId)
                .HasConstraintName("FK_JobCategories_JobCategories_ParentId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(jc => jc.Name)
                .IsRequired(true)
                .HasMaxLength(255);
        }
    }
}
