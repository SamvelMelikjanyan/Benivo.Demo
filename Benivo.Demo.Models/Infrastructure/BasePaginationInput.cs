namespace Benivo.Demo.Models.Infrastructure
{
    public class BasePaginationInput : BaseInput
    {
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
