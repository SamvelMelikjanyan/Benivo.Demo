using Benivo.Demo.Entities.Infrastructure;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class EmploymentType : BaseEntity
    {
        public EmploymentType()
        {
            JobAnnouncements = new HashSet<JobAnnouncement>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<JobAnnouncement> JobAnnouncements { get; set; }
    }
}
