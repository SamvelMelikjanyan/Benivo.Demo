using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Outputs
{
    public class SearchJobAnnouncementOutput : BaseOutput
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public short CountryId { get; set; }

        public int CityId { get; set; }

        public long CompanyId { get; set; }

        public byte EmploymentTypeId { get; set; }

        public bool IsBookmarked { get; set; }

        public bool IsApplied { get; set; }
    }
}
