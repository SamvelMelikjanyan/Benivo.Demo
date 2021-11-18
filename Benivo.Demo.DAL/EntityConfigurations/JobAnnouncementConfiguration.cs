using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class JobAnnouncementConfiguration : EntityConfiguration<JobAnnouncement>
    {
        public override void Configure(EntityTypeBuilder<JobAnnouncement> builder)
        {
            base.Configure(builder);

            builder.ToTable("JobAnnouncements");

            builder.HasKey(ja => ja.Id)
                .IsClustered(true)
                .HasName("PK_JobAnnouncements_Id");

            builder.HasOne(ja => ja.EmploymentType)
                .WithMany(jt => jt.JobAnnouncements)
                .HasForeignKey(ja => ja.EmploymentTypeId)
                .HasConstraintName("FK_JobAnnouncements_EmployementTypes_EmployementTypeId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ja => ja.City)
                .WithMany(jt => jt.JobAnnouncements)
                .HasForeignKey(ja => ja.CityId)
                .HasConstraintName("FK_JobAnnouncements_Cities_CityId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ja => ja.Company)
                .WithMany(jt => jt.JobAnnouncements)
                .HasForeignKey(ja => ja.CompanyId)
                .HasConstraintName("FK_JobAnnouncements_Company_CompanyId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ja => ja.JobCategory)
                .WithMany(jt => jt.JobAnnouncements)
                .HasForeignKey(ja => ja.JobCategoryId)
                .HasConstraintName("FK_JobAnnouncements_JobCategories_JobCategoryId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ja => ja.Title)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(ja => ja.Description)
                .HasColumnType("nvarchar(max)")
                .IsRequired(true);
        }
    }
}
