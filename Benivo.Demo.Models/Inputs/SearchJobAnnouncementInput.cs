using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Inputs
{
    public class SearchJobAnnouncementInput : BasePaginationInput
    {
        public string Title { get; set; }

        public short? JobCategoryId { get; set; }

        public int? Location { get; set; }

        public byte? EmploymentTypeId { get; set; }
    }
}
