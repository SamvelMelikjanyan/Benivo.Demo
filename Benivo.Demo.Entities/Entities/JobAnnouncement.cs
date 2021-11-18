using Benivo.Demo.Entities.Infrastructure;
using System;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class JobAnnouncement : BaseEntity
    {
        public JobAnnouncement()
        {
            UserJobAnnouncements = new HashSet<UserJobAnnouncement>();
        }

        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public byte EmploymentTypeId { get; set; }

        public int CityId { get; set; }

        public long CompanyId { get; set; }

        public short JobCategoryId { get; set; }

        public virtual EmploymentType EmploymentType { get; set; }

        public virtual City City { get; set; }

        public virtual Company Company { get; set; }

        public virtual JobCategory JobCategory { get; set; }

        public virtual ICollection<UserJobAnnouncement> UserJobAnnouncements { get; set; }
    }
}
