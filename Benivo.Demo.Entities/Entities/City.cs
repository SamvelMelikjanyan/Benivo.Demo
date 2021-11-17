using Benivo.Demo.Entities.Infrastructure;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class City : BaseEntity
    {
        public City()
        {
            JobAnnouncements = new HashSet<JobAnnouncement>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public short CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<JobAnnouncement> JobAnnouncements { get; set; }
    }
}
