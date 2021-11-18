using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Outputs
{
    public class GetJobCategoriesOutput : BaseOutput
    {
        public short Id { get; set; }

        public short? ParentId { get; set; }

        public bool HasChild { get; set; }
    }
}
