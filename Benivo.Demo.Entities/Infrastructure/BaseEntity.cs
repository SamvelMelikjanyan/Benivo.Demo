using System;

namespace Benivo.Demo.Entities.Infrastructure
{
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }
}
