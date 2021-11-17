using Benivo.Demo.Entities.Infrastructure;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            JobAnnouncements = new HashSet<JobAnnouncement>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<JobAnnouncement> JobAnnouncements { get; set; }
    }
}
