using Benivo.Demo.Entities.Infrastructure;
using System.Collections.Generic;

namespace Benivo.Demo.Entities.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserJobAnnouncements = new HashSet<UserJobAnnouncement>();
        }

        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<UserJobAnnouncement> UserJobAnnouncements { get; set; }
    }
}
