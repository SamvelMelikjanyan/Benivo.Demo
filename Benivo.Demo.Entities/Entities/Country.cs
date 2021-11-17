using Benivo.Demo.Entities.Infrastructure;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class Country : BaseEntity
    {
        public Country()
        {
            Cities = new HashSet<City>();
            JobAnnouncements = new HashSet<JobAnnouncement>();
        }

        public short Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<JobAnnouncement> JobAnnouncements { get; set; }
    }
}
