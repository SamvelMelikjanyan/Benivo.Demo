using Benivo.Demo.Entities.Infrastructure;

namespace Benivo.Demo.Entities.Entities
{
    public class UserJobAnnouncement : BaseEntity
    {
        public long UserId { get; set; }

        public long JobAnnouncementId { get; set; }

        public bool IsBookmarked { get; set; }

        public bool IsApplied { get; set; }

        public virtual User User { get; set; }

        public virtual JobAnnouncement JobAnnouncement { get; set; }
    }
}
