using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Outputs
{
    public class GetEmploymentTypeOutput : BaseOutput
    {
        public byte Id { get; set; }

        public string Name { get; set; }
    }
}
