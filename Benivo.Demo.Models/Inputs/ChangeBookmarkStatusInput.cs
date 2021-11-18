using Benivo.Demo.Models.Infrastructure;

namespace Benivo.Demo.Models.Inputs
{
    public class ChangeBookmarkStatusInput : BaseInput
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public bool IsBookmarked { get; set; }
    }
}
