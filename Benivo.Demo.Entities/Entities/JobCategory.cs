using Benivo.Demo.Entities.Infrastructure;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class JobCategory : BaseEntity
    {
        public JobCategory()
        {
            SubCategories = new HashSet<JobCategory>();
            JobAnnouncements = new HashSet<JobAnnouncement>();
        }

        public short Id { get; set; }

        public string Name { get; set; }

        public short? ParentId { get; set; }

        public virtual JobCategory Parent { get; set; }

        public virtual ICollection<JobCategory> SubCategories { get; set; }

        public virtual ICollection<JobAnnouncement> JobAnnouncements { get; set; }
    }
}
