using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Benivo.Demo.DAL.EntityConfigurations
{
    internal class UserJobAnnouncementConfiguration : EntityConfiguration<UserJobAnnouncement>
    {
        public override void Configure(EntityTypeBuilder<UserJobAnnouncement> builder)
        {
            base.Configure(builder);

            builder.ToTable("UserJobAnnouncements");

            builder.HasKey(uja => new { uja.UserId, uja.JobAnnouncementId })
                .IsClustered(true)
                .HasName("PK_UserJobAnnouncements_UserId_JobAnnouncementId");

            builder.HasOne(uja => uja.User)
                .WithMany(u => u.UserJobAnnouncements)
                .HasForeignKey(uja => uja.UserId)
                .HasConstraintName("FK_UserJobAnnouncements_User_UserId");

            builder.HasOne(uja => uja.JobAnnouncement)
                .WithMany(u => u.UserJobAnnouncements)
                .HasForeignKey(uja => uja.JobAnnouncementId)
                .HasConstraintName("FK_UserJobAnnouncements_JobAnnouncements_JobAnnouncementId");

            builder.Property(uja => uja.IsBookmarked)
                .HasDefaultValue(false);

            builder.Property(uja => uja.IsApplied)
                .HasDefaultValue(false);
        }
    }
}
