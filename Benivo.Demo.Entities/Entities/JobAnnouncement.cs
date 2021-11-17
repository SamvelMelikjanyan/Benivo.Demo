using Benivo.Demo.Entities.Infrastructure;
using System;

namespace Benivo.Demo.Entities.Entities
{
    public class JobAnnouncement : BaseEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public byte JobTypeId { get; set; }

        public short CountryId { get; set; }

        public int CityId { get; set; }

        public long CompanyId { get; set; }

        public virtual JobType JobType { get; set; }

        public virtual Country Country { get; set; }

        public virtual City City { get; set; }

        public virtual Company Company { get; set; }
    }
}
