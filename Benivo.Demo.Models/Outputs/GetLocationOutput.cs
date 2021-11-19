using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Outputs
{
    public class GetLocationOutput : BaseOutput
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
