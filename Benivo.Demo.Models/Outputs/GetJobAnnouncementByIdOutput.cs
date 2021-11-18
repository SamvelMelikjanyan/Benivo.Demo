using Benivo.Demo.Models.Infrastructure;
using System;

namespace Benivo.Demo.Models.Outputs
{
    public class GetJobAnnouncementByIdOutput : BaseOutput
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public byte EmploymentTypeId { get; set; }

        public int CityId { get; set; }

        public long CompanyId { get; set; }

        public short JobCategoryId { get; set; }

        public bool IsBookmarked { get; set; }

        public bool IsApplied { get; set; }
    }
}
